
using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Infrastructure.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardsServer.API.Controllers
{
    /// <summary>
    /// Апи для получения разрешений
    /// </summary>
    /// <param name="permissionService"></param>
    [ApiController]
    [Authorize]
    public class PermissionController(IPermissionService permissionService) : ControllerBase
    {
        [HttpGet("permission/get")]
        public async Task<IActionResult> GetPermissions(CancellationToken cancellationToken)
        {
            int userId = User.GetId();
            
            IEnumerable<string> result = await permissionService.GetPermissions(userId, cancellationToken);
            
            return Ok(result);
        }
    }
}