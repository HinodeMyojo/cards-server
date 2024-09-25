using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto;
using CardsServer.BLL.Dto.Login;
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

        [HttpGet("ping")]
        public async Task<IActionResult> TestConnection()
        {
            return Ok("Hinode!");
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(
            RegisterUser model, CancellationToken cancellationToken)
        {
            Result result = await _service.RegisterUser(model, cancellationToken);

            return result.ToActionResult();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(
            LoginUser user, CancellationToken cancellationToken)
        {
            Result<string> result = await _service.LoginUser(user, cancellationToken);

            return result.ToActionResult();
        }

        [HttpPost("email-send")]
        public async Task<IActionResult> SendEmail(
            [FromForm] SendMailDto model, IFormFileCollection? file, CancellationToken cancellationToken)
        {
            if (file != null && file.Any())
            {
                _publisher.SendEmailWithFiles(model, file);
            }
            else
            {
                _publisher.SendEmail(model);
            }


            return Ok($"Сообщение на адрес: {model.To} успешно отправлено! Введите код из письма");
            //return BadRequest("Нет");
            //if (userId == null || code == null)
            //{
            //    return View("Error");
            //}
            //var user = await _userManager.FindByIdAsync(userId);
            //if (user == null)
            //{
            //    return View("Error");
            //}
            //var result = await _userManager.ConfirmEmailAsync(user, code);
            //if (result.Succeeded)
            //    return RedirectToAction("Index", "Home");
            //else
            //    return View("Error");
        }
    }
}
