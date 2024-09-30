using CardsServer.BLL.Entity;

namespace CardsServer.BLL.Abstractions
{
    public interface IPolicyRepository
    {
        Task<RoleEntity[]> GetRolesAsync(int userId);
    }
}