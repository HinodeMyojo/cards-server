using CardsServer.BLL.Entity;

namespace CardsServer.BLL.Abstractions
{
    public interface IPermissionReposotory
    {
        Task<IEnumerable<PermissionEntity>> Get(int userId, CancellationToken cancellationToken);
    }
}