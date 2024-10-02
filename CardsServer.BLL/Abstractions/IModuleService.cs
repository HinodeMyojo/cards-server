using CardsServer.BLL.Dto.Module;
using CardsServer.BLL.Infrastructure.Result;

namespace CardsServer.BLL.Abstractions
{
    public interface IModuleService
    {
        Task<Result<int>> CreateModule(int userId, CreateModule module, CancellationToken cancellationToken);
    }
}