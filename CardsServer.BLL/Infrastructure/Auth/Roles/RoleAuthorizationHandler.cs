using CardsServer.BLL.Infrastructure.Auth.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsServer.BLL.Infrastructure.Auth.Roles
{
    public class RoleAuthorizationHandler : AuthorizationHandler<RoleRequirement>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RoleAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            RoleRequirement requirement)
        {
            string? userId = context.User.Claims.FirstOrDefault(
                x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;

            if (!int.TryParse(userId, out int parsedUserId))
            {
                return;
            }

            using IServiceScope scope = _serviceScopeFactory.CreateScope();

            IPolicyService permissionService = scope.ServiceProvider
                .GetRequiredService<IPolicyService>();

            HashSet<string> roles = await permissionService
                .GetRolesAsync(parsedUserId);

            if (roles.Contains(requirement.Role))
            {
                context.Succeed(requirement);
            }
        }
    }
}
