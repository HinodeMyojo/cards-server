using CardsServer.BLL.Entity;
namespace CardsServer.BLL.Abstractions.Specifications
{
    internal interface IPermissionService
    {
        internal bool CanCreateModule(UserEntity user, ModuleEntity module);
        internal bool CanEditModule(UserEntity user, ModuleEntity module);
        internal bool CanDeleteModule(UserEntity user, ModuleEntity module);
        internal bool CanGetModule(UserEntity user, ModuleEntity module);
    }
}
