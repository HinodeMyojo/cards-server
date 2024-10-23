using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace CardsServer.DAL.Repository
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly ApplicationContext _context;

        public ModuleRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddModuleToUsed(UserEntity user, CancellationToken cancellationToken)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
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

        public async Task<ICollection<ModuleEntity>> GetModules(
            int userId, 
            Expression<Func<ModuleEntity, bool>> expression, 
            CancellationToken cancellationToken)
        {
            return await _context.Modules
                .Include(x => x.Elements)
                .ThenInclude(x => x.Image)
                .Where(expression)
                .ToListAsync();

        }
    }
}
