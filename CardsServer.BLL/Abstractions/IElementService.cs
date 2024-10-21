using CardsServer.BLL.Dto.Element;

namespace CardsServer.BLL.Abstractions
{
    public interface IElementService
    {
        Task<GetElement?> GetElement(int moduleId, CancellationToken cancellationToken);
    }
}