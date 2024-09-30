using CardsServer.BLL.Infrastructure.Auth;
using CardsServer.BLL.Infrastructure.Auth.Attrubutes;
using CardsServer.BLL.Infrastructure.Auth.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardsServer.API.Controllers
{
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        [HasRole(Role.Admin)]
        [HttpDelete("user/delete/{id}")]
        public async Task<IActionResult> DeleteUser()
        {
            return Ok();
        }

        [HttpDelete("user/deletee/{id}")]
        public async Task<IActionResult> Biba()
        {
            return Ok();
        }

        [HttpDelete("user/deletehbe/{id}")]
        public async Task<IActionResult> BiFfba()
        {
            return Ok();
        }

        //[HasPermission(Permission.CreateObjects)]
        //[HttpPost("user/block/{id}")]
        //public async Task<IActionResult> BlockUser()
        //{
        //    return Ok();
        //}
    }
}
