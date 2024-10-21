using CardsServer.BLL.Dto.Module;
using CardsServer.BLL.Entity;

namespace CardsServer.BLL.Abstractions
{
    public interface IModuleRepository
    {
        Task<int> CreateModule(ModuleEntity entity, CancellationToken cancellationToken);
        Task<ModuleEntity?> GetModule(int id, CancellationToken cancellationToken);
        Task<ICollection<ModuleEntity>> GetUsedModules(int userId, CancellationToken cancellationToken);
    }
}