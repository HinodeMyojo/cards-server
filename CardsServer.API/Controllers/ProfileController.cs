using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto.Profile;
using CardsServer.BLL.Infrastructure.Auth;
using CardsServer.BLL.Infrastructure.Auth.Attrubutes;
using CardsServer.BLL.Infrastructure.Result;
using CardsServer.BLL.Services.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardsServer.API.Controllers;

[Authorize]
[ApiController]
[Route("api")]
public class ProfileController : ControllerBase
{
       private readonly IProfileSerivce _profileService;

       public ProfileController(IProfileSerivce profileService)
       {
              _profileService = profileService;
       }


       [HttpGet("profile/access")]
       public async Task<IActionResult> GetAccess(string requestedUserName, CancellationToken cancellationToken)
       {
            int UserId = User.GetId();
              
            Result<GetProfileSimpleAccess> result = await _profileService
                    .GetAccess(requestedUserName, UserId, cancellationToken);
              
            return Ok();
       }

       /// <summary>
       /// TODO block user (admin/moderator)
       /// </summary>
       /// <param name="userName"></param>
       /// <param name="cancellationToken"></param>
       /// <returns></returns>
       [HttpGet("profile/block")]
       public async Task<IActionResult> Block(string userName, CancellationToken cancellationToken)
       {
            return Ok();
       }

       /// <summary>
       /// TODO Delete profile
       /// </summary>
       /// <param name="requestedUserName"></param>
       /// <param name="cancellationToken"></param>
       /// <returns></returns>
       [HttpDelete("profile")]
       public async Task<IActionResult> DeleteProfile(string requestedUserName, CancellationToken cancellationToken)
       {
            return Ok();  
       }
}