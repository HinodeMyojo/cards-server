using CardsServer.BLL.Entity;

namespace CardsServer.DAL.Repository
{
    public interface IImageRepository
    {
        Task<int> AddImage(ImageEntity image);
        Task<byte[]?> GetImage(int id, CancellationToken cancellationToken);
    }
}