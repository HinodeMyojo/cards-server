using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto;
using CardsServer.BLL.Dto.Login;
using CardsServer.BLL.Infrastructure.RabbitMq;
using CardsServer.BLL.Infrastructure.Result;
using Microsoft.AspNetCore.Mvc;

namespace CardsServer.API.Controllers
{
    [ApiController]
    public sealed class LoginController : ControllerBase
    {
        private readonly ILoginService _service;
        private readonly IRabbitMQPublisher _publisher;

        public LoginController(ILoginService service, IRabbitMQPublisher publisher)
        {
            _service = service;
            _publisher = publisher;
        }

        /// <summary>
        /// Проверка соединения с сервером
        /// </summary>
        /// <returns>Сообщение "Hinode!" при успешном ответе</returns>
        [HttpGet("auth/ping")]
        public async Task<IActionResult> TestConnection()
        {
            return Ok("Hinode!");
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="model">Модель с данными пользователя для регистрации</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Результат операции (успех или описание ошибки)</returns>
        [HttpPost("auth/register")]
        public async Task<IActionResult> RegisterUser(
            RegisterUser model, CancellationToken cancellationToken)
        {
            Result result = await _service.RegisterUser(model, cancellationToken);

            return result.ToActionResult();

        }
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="user">Данные для авторизации (логин и пароль)</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Токен доступа или описание ошибки</returns>
        [HttpPost("auth/login")]
        public async Task<IActionResult> LoginUser(
            LoginUserExtension user, CancellationToken cancellationToken)
        {
            Result<TokenApiModel> result = await _service.LoginUser(user, cancellationToken);

            return result.ToActionResult();
        }

        /// <summary>
        /// Обновление токена доступа
        /// </summary>
        /// <param name="tokenApiModel">Модель с текущим токеном</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Новый токен доступа</returns>
        [HttpPost("auth/refresh")]
        public async Task<IActionResult> RefreshToken(
            TokenApiModel tokenApiModel, CancellationToken cancellationToken)
        {
            Result<TokenApiModel> result = await _service.RefreshToken(tokenApiModel, cancellationToken);

            return result.ToActionResult();
        }

        /// <summary>
        /// Отправка кода восстановления пароля на email
        /// </summary>
        /// <param name="to">Email, на который будет отправлен код</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Результат операции с уведомлением об отправке</returns>
        [HttpGet("auth/send-recovery-code")]
        public async Task<IActionResult> SendRecoveryCode(
            string to, CancellationToken cancellationToken)
        {
            Result result = await _service.SendRecoveryCode(to, cancellationToken);

            return result.ToActionResult($"Письмо отправлено на {to}");
        }

        /// <summary>
        /// Проверка кода восстановления пароля
        /// </summary>
        /// <param name="email">Email пользователя</param>
        /// <param name="code">Код восстановления</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Результат операции с уведомлением об успехе или ошибке</returns>
        [HttpGet("auth/check-recovery-code")]
        public async Task<IActionResult> CheckRecoveryCode(
            string email, int code, CancellationToken cancellationToken)
        {
            Result result = await _service.CheckRecoveryCode(email, code, cancellationToken);

            return result.ToActionResult("Все успешно! Введите новый пароль");
        }

        /// <summary>
        /// Изменение пароля пользователем, который не авторизован
        /// </summary>
        /// <param name="model">Данные для обновления пароля (email, новый пароль, код восстановления)</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Результат операции с уведомлением об изменении пароля</returns>
        [HttpPost("auth/update-password")]
        public async Task<IActionResult> UpdatePassword(UserUpdatePasswordDto model, CancellationToken cancellationToken)
        {
            Result result = await _service.UpdatePassword(model, cancellationToken);

            return result.ToActionResult("Пароль изменен!"); 
        }
    }
}
