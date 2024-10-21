using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Entity;
using Microsoft.EntityFrameworkCore;

namespace CardsServer.DAL.Repository
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly ApplicationContext _context;

        public ModuleRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<int> CreateModule(ModuleEntity entity, CancellationToken cancellationToken)
        {
            await _context.Modules.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<ModuleEntity?> GetModule(int id, CancellationToken cancellationToken)
        {
            return await _context.Modules.Include(x => x.Elements).ThenInclude(x => x.Image).FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<ICollection<ModuleEntity>> GetUsedModules(int userId, CancellationToken cancellationToken)
        {
            return await _context.Modules
                .Include(x => x.Elements)
                .ThenInclude(x => x.Image)
                .Where(x => x.UsedUsers.Any(x => x.Id == userId))
                .ToListAsync();

        }
    }
}
