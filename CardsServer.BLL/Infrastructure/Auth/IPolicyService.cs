namespace CardsServer.BLL.Infrastructure.Auth
{
    public interface IPolicyService
    {
        Task<HashSet<string>> GetPermissionsAsync(int userId);
        Task<HashSet<string>> GetRolesAsync(int userId);
    }
}
