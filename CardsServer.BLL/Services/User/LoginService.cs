using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto;
using CardsServer.BLL.Dto.Login;
using CardsServer.BLL.Entity;
using CardsServer.BLL.Infrastructure;
using CardsServer.BLL.Infrastructure.Auth.Enums;
using CardsServer.BLL.Infrastructure.RabbitMq;
using CardsServer.BLL.Infrastructure.Result;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CardsServer.BLL.Services.User
{
    public sealed class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository; 
        private readonly ILoginRepository _loginRepository;
        private readonly ITokenService _tokenService;
        private readonly IRabbitMQPublisher _publisher;
        private readonly IRedisCaching _caching;
        private const string DEFAULT_FINGERPRINT = "default";
        //private readonly ILogger _logger;


        public LoginService(
            ILoginRepository loginRepository,
            ITokenService jwtGenerator,
            IUserRepository userRepository,
            IRabbitMQPublisher publisher,
            IRedisCaching caching
            //ILogger logger
            )
        {
            _loginRepository = loginRepository;
            _tokenService = jwtGenerator;
            _userRepository = userRepository;
            _publisher = publisher;
            _caching = caching;
            //_logger = logger;
        }
        public async Task<Result<TokenApiModel>> LoginUser(LoginUserExtension user, CancellationToken cancellationToken)
        {
            // FingerPrint - для логина на нескольких устройствах. Позволяет различать refresh токены.
            string fingerPrint = user.FingerPrint ?? DEFAULT_FINGERPRINT;

            // Получение пользователя из базы данных
            UserEntity? userFromDb = await _loginRepository.GetUser(user, cancellationToken);
            if (userFromDb == null)
            {
                return Result<TokenApiModel>.Failure("Пользователь не найден!");
            }

            // Проверка корректности модели
            var userValidationResult = AssertModel.CheckNull(userFromDb);
            if (!userValidationResult.IsSuccess)
            {
                return Result<TokenApiModel>.Failure(userValidationResult.Error);
            }

            // Проверка пароля
            if (!PasswordExtension.CheckPassword(userFromDb.Password, user.Password))
            {
                return Result<TokenApiModel>.Failure("Пароли не совпадают.");
            }

            // Удаление устаревших refresh токенов
            userFromDb.RefreshTokens.RemoveAll(rt => rt.FingerPrint == fingerPrint);

            // Генерация нового access и refresh токена
            var newAccessToken = _tokenService.GenerateAccessToken(userFromDb);
            var newRefreshToken = _tokenService.GetRefreshToken();

            // Добавление нового refresh токена в базу данных
            var now = DateTime.UtcNow;
            userFromDb.RefreshTokens.Add(new RefreshTokenEntity
            {
                Token = newRefreshToken,
                RefreshTokenExpiryTime = now.AddDays(30),
                FingerPrint = fingerPrint
            });

            await _userRepository.EditUser(userFromDb, cancellationToken);

            // Формирование результата
            var tokenApiModel = new TokenApiModel
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };

            return Result<TokenApiModel>.Success(tokenApiModel);
        }


        public async Task<Result> RegisterUser(RegisterUser model, CancellationToken cancellationToken)
        {
            if (await IsEmailUsedAsync(model.Email))
            {
                return Result.Failure("Пользователь с таким Email уже зарегистрирован!");
            }
            if(await IsLoginUsedAsync(model.UserName))
            {
                return Result.Failure("Пользователь с таким Username уже зарегистрирован!");
            }


            UserEntity user = new()
            {
                Email = model.Email,
                Password = PasswordExtension.HashPassword(model.Password),
                IsEmailConfirmed = false,
                RoleId = (int)Infrastructure.Auth.Enums.Role.User,
                StatusId = (int)Status.Active,
                UserName = model.UserName,
                CreatedAt = DateTime.UtcNow,
                AvatarId = 1 
            };

            await _loginRepository.RegisterUser(user, cancellationToken);

            return Result.Success();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result> SendRecoveryCode(string to, CancellationToken cancellationToken)
        {
            if (!await IsEmailUsedAsync(to))
            {
                return Result.Failure("Пользователя с таким Email не существует!");
            }
            UserEntity? user = await _userRepository.GetUser(user => user.UserName == to, cancellationToken);

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

        /// <summary>
        ///  Метод для обновления токена с помощью refresh токена
        /// </summary>
        /// <param name="tokenApiModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Result<TokenApiModel>> RefreshToken(TokenApiModel tokenApiModel, CancellationToken cancellationToken)
        {
            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;
            DateTime currentTime = DateTime.UtcNow;

            // Извлечение ClaimsPrincipal из токена
            ClaimsPrincipal principal;
            try
            {
                principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            }
            catch
            {
                return Result<TokenApiModel>.Failure("Невалидный токен доступа.");
            }

            // Извлечение UserId из токена
            if (!int.TryParse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
            {
                return Result<TokenApiModel>.Failure("Не удалось извлечь Id пользователя из токена.");
            }

            // Получение пользователя из базы данных
            var user = await _userRepository.GetUser(x => x.Id == userId, cancellationToken);
            if (user == null)
            {
                return Result<TokenApiModel>.Failure("Пользователь не найден.");
            }

            // Проверка существования refresh токена
            RefreshTokenEntity? refreshTokenEntity = user.RefreshTokens.FirstOrDefault(x => x.Token == refreshToken);
            if (refreshTokenEntity == null || refreshTokenEntity.RefreshTokenExpiryTime < currentTime)
            {
                return Result<TokenApiModel>.Failure("Недействительный или просроченный refresh токен.");
            }
            // Получаем fingerPrint хозяина токена
            string fingerPrint = refreshTokenEntity.FingerPrint;

            // Создание нового refresh токена
            var newRefreshToken = new RefreshTokenEntity
            {
                Token = _tokenService.GetRefreshToken(),
                RefreshTokenExpiryTime = _tokenService.GetRefreshTokenExpiryTime(),
                FingerPrint = fingerPrint
            };

            // Обновление токенов пользователя
            user.RefreshTokens.Remove(refreshTokenEntity);
            user.RefreshTokens.Add(newRefreshToken);

            await _userRepository.EditUser(user, cancellationToken);

            // Генерация нового access токена
            string newAccessToken = _tokenService.GenerateAccessToken(user);

            // Формирование результата
            TokenApiModel result = new()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken.Token
            };

            return Result<TokenApiModel>.Success(result);
        }


        public async Task<Result> CheckRecoveryCode(string email, int code, CancellationToken cancellationToken)
        {
            try
            {
                string? codeFromCache = await _caching.GetValueAsync(email);

                if (codeFromCache == null)
                {
                    return Result.Failure(new Error("Код не найден в кэше", statusCode: 404));
                }

                if (codeFromCache != code.ToString())
                {
                    return Result.Failure(new Error("Коды не совпали", statusCode: 400)); // Лучше 400 Bad Request, чем 423
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                //_logger.LogError(ex, "Ошибка при проверке кода восстановления");
                return Result.Failure(new Error("Внутренняя ошибка", statusCode: 500));
            }
        }

        public async Task<Result> UpdatePassword([FromBody]UserUpdatePasswordDto model, CancellationToken cancellationToken)
        {
            string newPassword = model.Password;
            string submitPassword = model.SubmitPassword;
            string email = model.Email;
            int code = model.Code;

            try
            {
                if (newPassword != submitPassword)
                {
                    return Result.Failure("Пароли не совпадают");
                }
                // Проверка кода восстановления
                var recoveryCodeCheck = await CheckRecoveryCode(email, code, cancellationToken);
                if (!recoveryCodeCheck.IsSuccess)
                {
                    return recoveryCodeCheck; // Возвращаем сразу, если код не валидный
                }

                // Поиск пользователя
                UserEntity? user = await _userRepository.GetUser(user => user.Email == email, cancellationToken);
                if (user == null)
                {
                    return Result.Failure(new Error("Пользователь с таким email не найден", statusCode: 404));
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
                return Result.Failure(new Error("Не удалось обновить пароль. Внутренняя ошибка", statusCode: 500));
            }
        }


        private async Task<bool> IsEmailUsedAsync(string email)
        {
            UserEntity? user = await _userRepository.GetUser( x => x.Email  == email, default);
            return user != null;
        }


        private async Task<bool> IsLoginUsedAsync(string userName)
        {
            UserEntity? user = await _userRepository.GetUser(x => x.UserName == userName, default);
            return user != null;
        }

    }
}
