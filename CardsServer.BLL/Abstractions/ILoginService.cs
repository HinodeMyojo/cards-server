using CardsServer.BLL.Dto;
using CardsServer.BLL.Dto.Login;
using CardsServer.BLL.Infrastructure.Result;

namespace CardsServer.BLL.Abstractions
{
    public interface ILoginService
    {
        Task<Result<string>> LoginUser(LoginUser user, CancellationToken cancellationToken);
        Task<Result> RegisterUser(RegisterUser model, CancellationToken cancellationToken);
        Task<Result> SendRecoveryCode(string to, CancellationToken cancellationToken);
    }
}