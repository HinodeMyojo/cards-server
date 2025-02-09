using CardsServer.BLL.Abstractions.Specifications;
using CardsServer.BLL.Entity;
using CardsServer.BLL.Enums;

namespace CardsServer.BLL.Services
{
    public class PermissionService : IPermissionService
    {
        public PermissionEnum GetEditPermissions(UserEntity user, ModuleEntity module)
        {
            if (HasPermission(user, PermissionEnum.CanEditAnyModule))
            {
                return PermissionEnum.CanEditAnyModule;
            }
            if (HasPermission(user, PermissionEnum.CanEditOwnModule) && module.CreatorId == user.Id)
            {
                return PermissionEnum.CanEditOwnModule;
            }
            
            throw new 
        }

        private bool HasPermission(UserEntity user, PermissionEnum permission)
        {
            if (user.Role == null || user.Role.Permissions == null)
                return false;
            return user.Role.Permissions.Any(p => p.Id == (int)permission);
        }
    }
}