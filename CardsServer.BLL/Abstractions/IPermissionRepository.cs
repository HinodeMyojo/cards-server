using CardsServer.BLL.Entity;

namespace CardsServer.BLL.Abstractions
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<PermissionEntity>> Get(int userId, CancellationToken cancellationToken);
    }
}