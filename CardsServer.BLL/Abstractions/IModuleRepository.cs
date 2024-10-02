using CardsServer.BLL.Entity;

namespace CardsServer.BLL.Abstractions
{
    public interface IModuleRepository
    {
        Task<int> CreateModule(ModuleEntity entity, CancellationToken cancellationToken);
    }
}