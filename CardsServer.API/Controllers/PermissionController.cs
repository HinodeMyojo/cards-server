
using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Infrastructure.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardsServer.API.Controllers
{
    [ApiController]
    [Authorize]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;
        [HttpGet("permission/get")]
        public async Task<IActionResult> GetPermissions(CancellationToken cancellationToken)
        {
            int userId = User.GetId();
            
            IEnumerable<string> result = await _permissionService.GetPermissions(userId, cancellationToken);
            
            return Ok(result);
        }
    }
}