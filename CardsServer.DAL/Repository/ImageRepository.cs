using CardsServer.BLL.Entity;
using Microsoft.EntityFrameworkCore;

namespace CardsServer.DAL.Repository
{
    public class ImageRepository : IImageRepository
    {
        private ApplicationContext _context;

        public ImageRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<int> AddImage(ElementImageEntity image)
        {
            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();

            return image.Id;
        }

        public async Task<byte[]?> GetImage(int id, CancellationToken cancellationToken)
        {
            ElementImageEntity? result = await _context.Images.FirstOrDefaultAsync(x => x.Id == id);
            return result?.Data;
        }
    }
}
