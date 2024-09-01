

using CardsServer.BLL.Entity;

namespace CardsServer.BLL.Abstractions
{
    public interface IPermissionRepository
    {
        Task<RoleEntity[]> GetPermissionsAsync(int userId);
    }
}