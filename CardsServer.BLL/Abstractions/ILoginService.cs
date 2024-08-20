using CardsServer.BLL.Dto;
using CardsServer.BLL.Dto.Login;

namespace CardsServer.BLL.Abstractions
{
    public interface ILoginService
    {
        Task<string> LoginUser(LoginUser user, CancellationToken cancellationToken);
        Task RegisterUser(RegisterUser model, CancellationToken cancellationToken);
    }
}