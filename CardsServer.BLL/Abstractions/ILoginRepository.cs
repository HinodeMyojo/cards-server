using CardsServer.BLL.Dto;
using CardsServer.BLL.Entity;

namespace CardsServer.BLL.Abstractions
{
    public interface ILoginRepository
    {
        Task RegisterUser(UserEntity model, CancellationToken cancellationToken);
    }
}
