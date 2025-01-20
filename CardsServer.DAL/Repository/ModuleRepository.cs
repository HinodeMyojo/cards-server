using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto.Module;
using CardsServer.BLL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq;
using System.Linq.Expressions;

namespace CardsServer.DAL.Repository
{
    public sealed class ModuleRepository : IModuleRepository
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

        public async Task DeleteModule(int id, CancellationToken cancellationToken)
        {
            ModuleEntity? module = await GetModule(id, cancellationToken);
            _context.Modules.Remove(module);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<ModuleEntity?> GetModule(int id, CancellationToken cancellationToken)
        {
            return await _context.Modules.Include(x => x.Creator).Include(x => x.Elements).ThenInclude(x => x.Image).FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<ICollection<ModuleEntity>> GetModules(
            int userId, 
            Expression<Func<ModuleEntity, bool>> expression, 
            CancellationToken cancellationToken)
        {
            return await _context.Modules
                .Include( x => x.UserModules)
                .Include(x => x.Elements)
                .ThenInclude(x => x.Image)
                .Where(expression)
                .ToListAsync();

        }

        public async Task<IEnumerable<ModuleEntity>> GetModules(GetModules model, CancellationToken cancellationToken)
        {
            switch (model)
            {
                case { AddElements: true, UserModules: false }:
                    return await _context.Modules
                        .Include(x => x.Elements)
                        .ThenInclude(x => x.Image)
                        .ToListAsync();
                case { AddElements: false, UserModules: true }:
                    return await _context.Modules
                        .Include(x => x.UserModules)
                        .ToListAsync();
                case { AddElements: true, UserModules: true }:
                    return await _context.Modules
                        .Include(x => x.UserModules)
                        .Include(x => x.Elements)
                        .ThenInclude(x => x.Image)
                        .ToListAsync();
                default:
                    return await _context.Modules.ToListAsync();
            }
        }

        public async Task<IEnumerable<ModuleEntity>> GetModulesShortInfo(int[] moduleIds, CancellationToken cancellationToken)
        {
            return await _context.Modules.Where(x => moduleIds.Contains(x.Id)).ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
