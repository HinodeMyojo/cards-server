using CardsServer.BLL.Dto.User;
using CardsServer.BLL.Infrastructure.Auth;
using CardsServer.BLL.Infrastructure.Auth.Attrubutes;
using CardsServer.BLL.Infrastructure.Auth.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CardsServer.API.Controllers
{
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        [HttpPatch("user/edit")]
        public async Task<IActionResult> EditUser(int id, [FromBody] JsonPatchDocument<PatchUser> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
