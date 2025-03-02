using CardsServer.BLL.Entity;

namespace CardsServer.BLL.Abstractions
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<PermissionEntity>> GetByRoleId(int roleId, CancellationToken cancellationToken);
        Task<IEnumerable<PermissionEntity>> Get(int[] userPermissionIds, CancellationToken cancellationToken);
    }
}