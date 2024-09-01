using CardsServer.BLL.Dto;
using CardsServer.BLL.Dto.Login;
using CardsServer.BLL.Entity;

namespace CardsServer.BLL.Abstractions
{
    public interface ILoginRepository
    {
        Task<UserEntity?> GetUser(LoginUser user, CancellationToken cancellationToken);
        Task RegisterUser(UserEntity model, CancellationToken cancellationToken);
    }
}
