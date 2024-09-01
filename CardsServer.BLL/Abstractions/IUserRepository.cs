using CardsServer.BLL.Entity;

namespace CardsServer.BLL.Abstractions
{
    public interface IUserRepository
    {
        Task<UserEntity?> GetUser(int userId, CancellationToken cancellationToken);
        Task<UserEntity?> GetUserByEmail(string email, CancellationToken cancellationToken);
    }
}
