using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto;
using CardsServer.BLL.Dto.Login;
using CardsServer.BLL.Infrastructure.Result;
using Microsoft.AspNetCore.Mvc;

namespace CardsServer.API.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _service;
        public LoginController(ILoginService service)
        {
            _service = service;
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

        [HttpGet("email-confirm")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            else
                return View("Error");
        }
    }
}
