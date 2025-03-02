using CardsServer.BLL.Enums;
using Microsoft.AspNetCore.Authorization;
namespace CardsServer.BLL.Infrastructure.Auth.Attrubutes
{
    public sealed class HasRoleAttribute : AuthorizeAttribute
    {
        public string POLICY_PREFIX = "HasRole";
        public HasRoleAttribute(RoleEnum roleEnum) : base(roleEnum.ToString())
        {
        }
    }
}
