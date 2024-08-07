using CardsServer.BLL.Dto;

namespace CardsServer.BLL.Abstractions
{
    public interface ILoginService
    {
        Task RegisterUser(RegisterUser model, CancellationToken cancellationToken);
    }
}