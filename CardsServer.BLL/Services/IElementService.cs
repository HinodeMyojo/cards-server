using CardsServer.BLL.Dto.Element;

namespace CardsServer.BLL.Services
{
    public interface IElementService
    {
        Task<GetElement?> GetElement(int moduleId, CancellationToken cancellationToken);
    }
}