using CardsServer.BLL.Entity;

namespace CardsServer.DAL.Repository
{
    public interface IElementRepostory
    {
        Task<int> AddElement(ElementEntity entity, CancellationToken cancellationToken);
        Task<ElementEntity?> GetElement(int id, CancellationToken cancellationToken);
        Task<ElementEntity?> GetElementByModuleId(int moduleId, CancellationToken cancellationToken);
        Task<List<ElementEntity>> GetElementsByModuleId(int moduleId, CancellationToken cancellationToken);
    }
}