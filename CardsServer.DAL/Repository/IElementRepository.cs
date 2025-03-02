using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CardsServer.DAL.Repository
{
    public sealed class ElementRepository(ApplicationContext context) : IElementRepository
    {
        public async Task<int> AddElement(ElementEntity entity, CancellationToken cancellationToken)
        {
            await context.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

        public async Task<ElementEntity?> GetElement(int id, CancellationToken cancellationToken) 
        {
            ElementEntity? element = await context.Elements.FindAsync(id, cancellationToken);
            return element;
        }

        public async Task<ElementEntity?> GetElementCreatorId(Expression<Func<ElementEntity, bool>> func, CancellationToken cancellationToken)
        {
            ElementEntity? element = await context.Elements.Include(x => x.Module).ThenInclude(x => x.Creator).FirstOrDefaultAsync(func, cancellationToken);

            return element;
        }

        public async Task<ElementEntity?> GetElementByModuleId(int moduleId, CancellationToken cancellationToken)
        {
            ElementEntity? element = await context.Elements.FirstOrDefaultAsync(x => x.ModuleId == moduleId, cancellationToken);
            return element;
        }

        public async Task<List<ElementEntity>> GetElementsByModuleId(int moduleId, CancellationToken cancellationToken)
        {
            List<ElementEntity> elements = await context.Elements.Where(x => x.ModuleId == moduleId).ToListAsync(cancellationToken: cancellationToken);
            return elements;
        }

        public async Task DeleteElementById(ElementEntity entity, CancellationToken cancellationToken)
        {
            context.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task EditElement(ElementEntity element, CancellationToken cancellationToken)
        {
            context.Update(element);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
