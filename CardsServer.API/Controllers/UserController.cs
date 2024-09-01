using CardsServer.BLL.Infrastructure.Auth;
using CardsServer.BLL.Infrastructure.Auth.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardsServer.API.Controllers
{
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        [HasPermission(Permission.ReadObjects)]
        [HttpDelete("user/read/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return Ok();
        }

        [HasPermission(Permission.CreateObjects)]
        [HttpPost("user/create/{id}")]
        public async Task<IActionResult> BlockUser(int id)
        {
            return Ok();
        }
    }
}
