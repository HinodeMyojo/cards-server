using CardsServer.BLL.Entity;
using System.Linq.Expressions;

namespace CardsServer.DAL.Repository
{
    public interface IElementRepostory
    {
        Task<int> AddElement(ElementEntity entity, CancellationToken cancellationToken);
        Task<ElementEntity?> GetElement(int id, CancellationToken cancellationToken);
        Task<ElementEntity?> GetElementCreatorId(Expression<Func<ElementEntity, bool>> func, CancellationToken cancellationToken);
        Task<ElementEntity?> GetElementByModuleId(int moduleId, CancellationToken cancellationToken);
        Task<List<ElementEntity>> GetElementsByModuleId(int moduleId, CancellationToken cancellationToken);
        Task DeleteElementById(ElementEntity entity, CancellationToken cancellationToken);
        Task EditElement(ElementEntity element, CancellationToken cancellationToken);
    }
}