using CardsServer.BLL.Dto;
using CardsServer.BLL.Dto.Login;
using CardsServer.BLL.Infrastructure.Result;

namespace CardsServer.BLL.Abstractions
{
    public interface ILoginService
    {
        Task<Result<string>> LoginUser(LoginUser user, CancellationToken cancellationToken);

        //Task<string> LoginUser(LoginUser user, CancellationToken cancellationToken);
        Task RegisterUser(RegisterUser model, CancellationToken cancellationToken);
    }
}