using CardsServer.BLL.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardsServer.API.Controllers
{
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login()
        {


            return Ok();

        }
    }
}
