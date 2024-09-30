using Microsoft.AspNetCore.Authorization;
namespace CardsServer.BLL.Infrastructure.Auth.Roles
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public string Role { get; }
        public RoleRequirement(string role)
        {
            Role = role;
        }
    }
}
