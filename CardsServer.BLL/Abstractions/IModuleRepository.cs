using CardsServer.BLL.Dto.Module;
using CardsServer.BLL.Entity;
using System.Linq.Expressions;

namespace CardsServer.BLL.Abstractions
{
    public interface IModuleRepository
    {
        Task<IEnumerable<ModuleEntity>> GetModulesShortInfo(int[] moduleIds, CancellationToken cancellationToken);
        Task AddModuleToUsed(UserEntity user, CancellationToken cancellationToken);
        Task<int> CreateModule(ModuleEntity entity, CancellationToken cancellationToken);
        Task DeleteModule(int id, CancellationToken cancellationToken);
        Task<ModuleEntity?> GetModule(int id, CancellationToken cancellationToken);
        Task<ICollection<ModuleEntity>> GetModules(int userId, Expression<Func<ModuleEntity, bool>> expression, CancellationToken cancellationToken);
    }
}