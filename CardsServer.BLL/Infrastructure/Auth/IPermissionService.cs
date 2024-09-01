namespace CardsServer.BLL.Infrastructure.Auth
{
    public interface IPermissionService
    {
        Task<HashSet<string>> GetPermissionsAsync(int userId);
    }
}
