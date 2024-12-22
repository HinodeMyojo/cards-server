using CardsServer.BLL.Dto.Module;
using CardsServer.BLL.Infrastructure.Result;

namespace CardsServer.BLL.Abstractions
{
    public interface IModuleService
    {
        Task<Result> AddModuleToUsed(int moduleId, int userId, CancellationToken cancellationToken);
        Task<Result<int>> CreateModule(int userId, CreateModule module, CancellationToken cancellationToken);
        Task<Result> DeleteModule(int userId, int id, CancellationToken cancellationToken);
        Task<Result<IEnumerable<GetModule>>> GetCreatedModules(int userId, CancellationToken cancellationToken);
        Task<Result<GetModule>> GetModule(int userId, int id, CancellationToken cancellationToken);
        Task<Result<IEnumerable<GetModule>>> GetUsedModules(int userId, string? textSearch, CancellationToken cancellationToken);
        Task<Result<IEnumerable<GetModule>>> GetModulesShortInfo(int[] moduleId, int userId, CancellationToken cancellationToken);
    }
}