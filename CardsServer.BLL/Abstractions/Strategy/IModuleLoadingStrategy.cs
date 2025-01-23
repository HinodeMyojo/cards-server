using CardsServer.BLL.Dto.Module;
using CardsServer.BLL.Entity;

namespace CardsServer.BLL.Abstractions
{
    public interface IModuleLoadingStrategy
    {
        IQueryable<ModuleEntity> ConfigureQuery(IQueryable<ModuleEntity> query, GetModules model);
    }
}
