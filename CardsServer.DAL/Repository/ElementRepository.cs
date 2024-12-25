using CardsServer.BLL.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CardsServer.DAL.Repository
{
    public sealed class ElementRepository : IElementRepostory
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

        public async Task<ElementEntity?> GetElementCreatorId(Expression<Func<ElementEntity, bool>> func, CancellationToken cancellationToken)
        {
            ElementEntity? element = await _context.Elements.Include(x => x.Module).ThenInclude(x => x.Creator).FirstOrDefaultAsync(func, cancellationToken);

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

        public async Task DeleteElementById(ElementEntity entity, CancellationToken cancellationToken)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task EditElement(ElementEntity element, CancellationToken cancellationToken)
        {
            _context.Update(element);
            await _context.SaveChangesAsync();
        }
    }
}
