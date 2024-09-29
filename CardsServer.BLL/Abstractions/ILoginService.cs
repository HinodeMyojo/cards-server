using CardsServer.BLL.Dto;
using CardsServer.BLL.Dto.Login;
using CardsServer.BLL.Infrastructure.Result;

namespace CardsServer.BLL.Abstractions
{
    public interface ILoginService
    {
        Task<Result> CheckRecoveryCode(string email, int code, CancellationToken cancellationToken);
        Task<Result<string>> LoginUser(LoginUser user, CancellationToken cancellationToken);
        Task<Result> RegisterUser(RegisterUser model, CancellationToken cancellationToken);
        Task<Result> SendRecoveryCode(string to, CancellationToken cancellationToken);
        Task<Result> UpdatePassword(string email, int code, string password, CancellationToken cancellationToken);
    }
}