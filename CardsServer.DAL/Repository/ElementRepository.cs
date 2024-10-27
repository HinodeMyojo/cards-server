using CardsServer.BLL.Entity;
using Microsoft.EntityFrameworkCore;
namespace CardsServer.DAL.Repository
{
    public class ElementRepository : IElementRepostory
    {
        private readonly ApplicationContext _context;

        public ElementRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<int> AddElement(ElementEntity entity, CancellationToken cancellationToken)
        {
            await _context.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

        public async Task<ElementEntity?> GetElement(int id, CancellationToken cancellationToken) 
        {
            ElementEntity? element = await _context.Elements.FindAsync(id, cancellationToken);
            return element;
        }

        public async Task<ElementEntity?> GetElementByModuleId(int moduleId, CancellationToken cancellationToken)
        {
            ElementEntity? element = await _context.Elements.FirstOrDefaultAsync(x => x.ModuleId == moduleId, cancellationToken);
            return element;
        }

        public async Task<List<ElementEntity>> GetElementsByModuleId(int moduleId, CancellationToken cancellationToken)
        {
            List<ElementEntity> elements = await _context.Elements.Where(x => x.ModuleId == moduleId).ToListAsync();
            return elements;
        }
    }
}
