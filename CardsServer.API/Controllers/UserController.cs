using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto.User;
using CardsServer.BLL.Infrastructure.Result;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CardsServer.API.Controllers
{
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPatch("user/edit/")]
        public async Task<IActionResult> EditUser([FromBody] JsonPatchDocument<PatchUser> patchDoc, CancellationToken cancellationToken)
        {
            int id = 1;
            if (patchDoc == null)
            {
                return BadRequest();
            }

            Result<GetUserResponse> user = await _userService.GetUser(id, cancellationToken);

            if (!user.IsSuccess)
            {
                return NotFound();
            }

            Result result = await _userService.EditUser(id, patchDoc, cancellationToken);

            return result.ToActionResult();
        }
    }
}
