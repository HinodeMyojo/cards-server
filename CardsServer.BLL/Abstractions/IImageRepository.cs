using CardsServer.BLL.Entity;

namespace CardsServer.DAL.Repository
{
    public interface IImageRepository
    {
        Task<int> AddImage(ElementImageEntity image);
        Task<byte[]?> GetImage(int id, CancellationToken cancellationToken);
    }
}