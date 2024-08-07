using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto;
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

        [HttpPost("login")]
        public async Task<IActionResult> RegisterUser(
            RegisterUser model, CancellationToken cancellationToken)
        {
            await _service.RegisterUser(model, cancellationToken);

            return Ok();

        }
    }
}
