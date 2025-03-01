namespace CardsServer.BLL.Abstractions
{
    public interface IPermissionService
    {
        Task<IEnumerable<string>> GetPermissions(int userId, CancellationToken cancellationToken);
    }
}