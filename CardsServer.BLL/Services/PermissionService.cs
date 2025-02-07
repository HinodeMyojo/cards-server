using CardsServer.BLL.Abstractions.Specifications;
using CardsServer.BLL.Entity;

namespace CardsServer.BLL.Services
{
    internal class PermissionService : IPermissionService
    {
        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="user"></param>
        /// <param name="module"></param>
        /// <returns></returns>
        bool IPermissionService.CanCreateModule(UserEntity user, ModuleEntity module)
        {
            if (module.CreatorId == user.Id)
            {
                return true;
            }
            return false;
        }

        bool IPermissionService.CanDeleteModule(UserEntity user, ModuleEntity module)
        {
            throw new NotImplementedException();
        }

        bool IPermissionService.CanEditModule(UserEntity user, ModuleEntity module)
        {
            throw new NotImplementedException();
        }

        bool IPermissionService.CanGetModule(UserEntity user, ModuleEntity module)
        {
            throw new NotImplementedException();
        }
    }
}
