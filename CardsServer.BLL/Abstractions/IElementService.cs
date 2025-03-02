using CardsServer.BLL.Dto.Element;
using CardsServer.BLL.Infrastructure.Result;

namespace CardsServer.BLL.Abstractions
{
    public interface IElementService
    {
        Task<Result> AddElement(AddElementModel model, int userId, CancellationToken cancellationToken);
        Task<Result> DeleteElementById(int id, int userId, CancellationToken cancellationToken);
        Task<Result> DeleteElements(int[] ids, int userId, CancellationToken cancellationToken);
        Task<Result> EditElement(EditElementModel model, int userId, CancellationToken cancellationToken);
        Task<GetElement?> GetElement(int moduleId, CancellationToken cancellationToken);
        Task<Result<GetElement>> GetElementById(int id, int userId, CancellationToken cancellationToken);
        Task<Result<IEnumerable<GetElement>>> GetElementsByModuleId(int moduleId, CancellationToken cancellationToken);
    }
}