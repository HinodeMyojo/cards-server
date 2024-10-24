using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto;
using CardsServer.BLL.Dto.Login;
using CardsServer.BLL.Entity;
using CardsServer.BLL.Infrastructure.RabbitMq;
using CardsServer.BLL.Infrastructure.Result;
using Microsoft.AspNetCore.Mvc;

namespace CardsServer.API.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _service;
        private readonly IRabbitMQPublisher _publisher;
        public LoginController(ILoginService service, IRabbitMQPublisher publisher)
        {
            _service = service;
            _publisher = publisher;
        }

        [HttpGet("auth/ping")]
        public async Task<IActionResult> TestConnection()
        {
            return Ok("Hinode!");
        }

        [HttpPost("auth/register")]
        public async Task<IActionResult> RegisterUser(
            RegisterUser model, CancellationToken cancellationToken)
        {
            Result result = await _service.RegisterUser(model, cancellationToken);

            return result.ToActionResult();
        }

        [HttpPost("auth/login")]
        public async Task<IActionResult> LoginUser(
            LoginUser user, CancellationToken cancellationToken)
        {
            Result<string> result = await _service.LoginUser(user, cancellationToken);

            return result.ToActionResult();
        }

        [HttpGet("auth/send-recovery-code")]
        public async Task<IActionResult> SendRecoveryCode(
            string to, CancellationToken cancellationToken)
        {
            Result result = await _service.SendRecoveryCode(to, cancellationToken);

            return result.ToActionResult($"Письмо отправлено на {to}");
        }

        [HttpGet("auth/check-recovery-code")]
        public async Task<IActionResult> CheckRecoveryCode(
            string email, int code, CancellationToken cancellationToken)
        {
            Result result = await _service.CheckRecoveryCode(email, code, cancellationToken);

            return result.ToActionResult("Все успешно! Введите новый пароль");
        }

        /// <summary>
        /// Метод для изменениия пароля НЕавторизованным юзерам
        /// </summary>
        /// <param name="email"></param>
        /// <param name="code"></param>
        /// <param name="password"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("auth/update-password")]
        public async Task<IActionResult> UpdatePassword(string email, int code, string password, CancellationToken cancellationToken)
        {
            Result result = await _service.UpdatePassword(email, code, password, cancellationToken);

            return result.ToActionResult("Пароль изменен!");
        }
    }
}
