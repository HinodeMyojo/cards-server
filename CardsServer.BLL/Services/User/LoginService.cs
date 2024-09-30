using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto;
using CardsServer.BLL.Dto.Login;
using CardsServer.BLL.Entity;
using CardsServer.BLL.Infrastructure;
using CardsServer.BLL.Infrastructure.Auth.Enums;
using CardsServer.BLL.Infrastructure.RabbitMq;
using CardsServer.BLL.Infrastructure.Result;

namespace CardsServer.BLL.Services.User
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository; 
        private readonly ILoginRepository _loginRepository;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IRabbitMQPublisher _publisher;
        private readonly IRedisCaching _caching;
        //private readonly ILogger _logger;


        public LoginService(
            ILoginRepository loginRepository,
            IJwtGenerator jwtGenerator,
            IUserRepository userRepository,
            IRabbitMQPublisher publisher,
            IRedisCaching caching
            //ILogger logger
            )
        {
            _loginRepository = loginRepository;
            _jwtGenerator = jwtGenerator;
            _userRepository = userRepository;
            _publisher = publisher;
            _caching = caching;
            //_logger = logger;
        }
        public async Task<Result<string>> LoginUser(LoginUser user, CancellationToken cancellationToken)
        {

            UserEntity? res = await _loginRepository.GetUser(user, cancellationToken);
            Result<UserEntity> userResult = AssertModel.CheckNull(res);
            // спрятать в AssertModel
            if (!userResult.IsSuccess)
            {
                return Result<string>.Failure(userResult.Error, userResult.StatusCode);
            }

            if (!PasswordExtension.CheckPassword(res.Password, user.Password))
            {
                return Result<string>.Failure("Пароли не сопадают.");
            }

            string token = _jwtGenerator.GenerateToken(res);
            return Result<string>.Success(token);
        }
        public async Task<Result> RegisterUser(RegisterUser model, CancellationToken cancellationToken)
        {
            if (await IsEmailUsedAsync(model.Email))
            {
                return Result.Failure("Пользователь с таким Email уже зарегистрирован!");
            }

            UserEntity user = new()
            {
                Email = model.Email,
                Password = PasswordExtension.HashPassword(model.Password),
                IsEmailConfirmed = false,
                RoleId = (int)Infrastructure.Auth.Enums.Role.User,
                StatusId = (int)Status.Active,
                UserName = model.UserName,
            };

            await _loginRepository.RegisterUser(user, cancellationToken);

            return Result.Success();
        }

        public async Task<Result> SendRecoveryCode(string to, CancellationToken cancellationToken)
        {
            if (!await IsEmailUsedAsync(to))
            {
                return Result.Failure("Пользователя с таким Email не существует!");
            }
            UserEntity? user = await _userRepository.GetUserByEmail(to, cancellationToken);

            int recoveryCode = RandomExtension.GenerateRecoveryCode();

            user.RecoveryCode = recoveryCode;

            try
            {
                await _caching.SetValueAsync(to, recoveryCode.ToString());
            }
            catch
            {
                return Result.Failure("Сервис кэширования не доступен");
            }
            //await _userRepository.EditUser(user, cancellationToken);

            SendMailDto mail = new()
            {
                To = [to],
                Subject = "Восстановление пароля pleiades.ru",
                Content = $"Ваш код восстановления к аккунту {recoveryCode.ToString()}."
            };

            _publisher.SendEmail(mail);

            return Result.Success();
        }

        public async Task<Result> CheckRecoveryCode(string email, int code, CancellationToken cancellationToken)
        {
            try
            {
                string? codeFromCache = await _caching.GetValueAsync(email);

                if (codeFromCache == null)
                {
                    return Result.Failure("Код не найден в кэше", statusCode: 404);
                }

                if (codeFromCache != code.ToString())
                {
                    return Result.Failure("Коды не совпали", statusCode: 400); // Лучше 400 Bad Request, чем 423
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                //_logger.LogError(ex, "Ошибка при проверке кода восстановления");
                return Result.Failure("Внутренняя ошибка", statusCode: 500);
            }
        }

        public async Task<Result> UpdatePassword(string email, int code, string newPassword, CancellationToken cancellationToken)
        {
            try
            {
                // Проверка кода восстановления
                var recoveryCodeCheck = await CheckRecoveryCode(email, code, cancellationToken);
                if (!recoveryCodeCheck.IsSuccess)
                {
                    return recoveryCodeCheck; // Возвращаем сразу, если код не валидный
                }

                // Поиск пользователя
                UserEntity? user = await _userRepository.GetUserByEmail(email, cancellationToken);
                if (user == null)
                {
                    return Result.Failure("Пользователь с таким email не найден", statusCode: 404);
                }

                user.Password = PasswordExtension.HashPassword(newPassword);

                // Обновление пользователя в базе
                await _userRepository.EditUser(user, cancellationToken);

                return Result.Success();
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                //_logger.LogError(ex, "Ошибка при обновлении пароля для пользователя с email: {email}", email);
                return Result.Failure("Не удалось обновить пароль. Внутренняя ошибка", statusCode: 500);
            }
        }


        private async Task<bool> IsEmailUsedAsync(string email)
        {
            UserEntity? user = await _userRepository.GetUserByEmail(email, default);
            return user != null;
        }

        
    }
}
