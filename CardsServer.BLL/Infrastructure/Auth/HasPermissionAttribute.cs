using System.Web.Http;

namespace CardsServer.BLL.Infrastructure.Auth
{
    public sealed class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(Permission permission)
        {
            Policy = permission.ToString();
        }
    }
}
