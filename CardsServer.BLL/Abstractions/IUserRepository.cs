using CardsServer.BLL.Dto.User;

namespace CardsServer.BLL.Abstractions
{
    public interface IUserRepository
    {
        Task<GetUserResponse> GetUser(int userId, CancellationToken cancellationToken);
    }
}
