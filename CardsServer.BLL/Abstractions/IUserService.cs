using CardsServer.BLL.Dto.User;

namespace CardsServer.BLL.Abstractions
{
    public interface IUserService
    {
        Task<GetUserResponse> GetUser(int userId, CancellationToken cancellationToken);
    }
}