using CardsServer.BLL.Infrastructure.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardsServer.API.Controllers
{
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        [HasPermission(Permission.AccessMembers)]
        [HttpDelete("user/delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return Ok();
        }

        [HasPermission(Permission.ReadMember)]
        [HttpPost("user/block/{id}")]
        public async Task<IActionResult> BlockUser(int id)
        {
            return Ok();
        }
    }
}
