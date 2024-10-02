using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Entity;

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
    }
}
