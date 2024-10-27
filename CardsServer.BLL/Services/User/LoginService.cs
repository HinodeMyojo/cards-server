﻿using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto;
using CardsServer.BLL.Dto.Login;
using CardsServer.BLL.Entity;
using CardsServer.BLL.Infrastructure;
using CardsServer.BLL.Infrastructure.Auth.Enums;
using CardsServer.BLL.Infrastructure.RabbitMq;
using CardsServer.BLL.Infrastructure.Result;
using System.Security.Claims;

namespace CardsServer.BLL.Services.User
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository; 
        private readonly ILoginRepository _loginRepository;
        private readonly ITokenService _tokenService;
        private readonly IRabbitMQPublisher _publisher;
        private readonly IRedisCaching _caching;
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
        public async Task<Result<TokenApiModel>> LoginUser(LoginUser user, CancellationToken cancellationToken)
        {

            UserEntity? res = await _loginRepository.GetUser(user, cancellationToken);
            Result<UserEntity> userResult = AssertModel.CheckNull(res);
            // спрятать в AssertModel
            if (!userResult.IsSuccess)
            {
                return Result<TokenApiModel>.Failure(userResult.Error);
            }

            if (!PasswordExtension.CheckPassword(res.Password, user.Password))
            {
                return Result<TokenApiModel>.Failure("Пароли не сопадают.");
            }
            TokenApiModel result = new()
            {
                AccessToken = _tokenService.GenerateToken(res),
                RefreshToken = _tokenService.GetRefreshToken()
            };

            return Result<TokenApiModel>.Success(result);
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
                AvatarId = 1 
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

        public async Task<Result<TokenApiModel>> RefreshToken(TokenApiModel tokenApiModel, CancellationToken cancellationToken)
        {
            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;

            // Получаем Claims из старого access токена
            ClaimsPrincipal principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

            Claim? userIdClaim = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            int userId;

            try
            {
                if (userIdClaim != null)
                {
                    int.TryParse(userIdClaim.Value, out userId);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch 
            {
                throw new Exception("Не удалось распарсить токен пользователя. Кто-то кого-то решил наебать -_-");
            }

            UserEntity? user = await _userRepository.GetUser(userId, cancellationToken);
            if (user == null)
            {
                return Result<TokenApiModel>.Failure(ErrorAdditional.Forbidden);
            }
            else
            {
                
            }

            return (Result<TokenApiModel>)Result<TokenApiModel>.Success();



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
            UserEntity? user = await _userRepository.GetUserByEmail(email, default);
            return user != null;
        }

        
    }
}
