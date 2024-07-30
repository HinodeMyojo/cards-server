using Microsoft.AspNetCore.Mvc;

namespace CardsServer.API.Controllers
{
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
