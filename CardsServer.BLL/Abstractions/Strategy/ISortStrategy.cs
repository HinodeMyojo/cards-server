using CardsServer.BLL.Entity;

namespace CardsServer.BLL.Abstractions.Strategy
{
    public interface ISortStrategy
    {
        IQueryable<ModuleEntity> ApplySort(IQueryable<ModuleEntity> query);
    }
}
