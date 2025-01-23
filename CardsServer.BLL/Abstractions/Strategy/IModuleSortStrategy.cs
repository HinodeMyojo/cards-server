using CardsServer.BLL.Entity;

namespace CardsServer.BLL.Abstractions.Strategy
{
    public interface IModuleSortStrategy
    {
        IQueryable<ModuleEntity> ApplySort(IQueryable<ModuleEntity> query);
    }
}
