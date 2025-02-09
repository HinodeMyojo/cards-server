using CardsServer.BLL.Entity;
using CardsServer.BLL.Enums;

namespace CardsServer.BLL.Abstractions.Specifications
{
    public interface IPermissionService
    {
        public PermissionEnum GetEditPermissions(UserEntity user, ModuleEntity module);
    }
}
