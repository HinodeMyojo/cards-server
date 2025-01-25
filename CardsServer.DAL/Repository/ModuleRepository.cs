using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto.Module;
using CardsServer.BLL.Entity;
using CardsServer.BLL.Enums;
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
            const int DEFAULT_LIMIT = 50;
            const int DAYS_IN_WEEK = 7;
            const int HALF_OF_YEAR = 182;

            IQueryable<ModuleEntity> query;
            
            switch (model)
            {
                case { AddElements: true, UserModules: false }:
                    query = _context.Modules
                        .Include(x => x.Elements)
                        .ThenInclude(x => x.Image);
                    break; 
                case { AddElements: false, UserModules: true }:
                    query = _context.Modules
                        .Include(x => x.UserModules);
                    break;  
                case { AddElements: true, UserModules: true }:
                    query = _context.Modules
                        .Include(x => x.UserModules)
                        .Include(x => x.Elements)
                        .ThenInclude(x => x.Image);
                    break;  
                default:
                    query = _context.Modules;
                    break;
            }

            query = model.SortOption switch
            {
                SortOptionEnum.Newest => query.OrderByDescending(x => x.CreateAt),
                SortOptionEnum.Oldest => query.OrderBy(x => x.CreateAt),
                SortOptionEnum.Popularity => query.OrderByDescending(x => x.UserModules.Count),
                _ => query.OrderByDescending(x => x.UserModules.Count)
            };

            switch (model.SortTime)
            {
                case SortTimeEnum.Day:
                    query = query.Where(x => x.CreateAt.Date == DateTime.UtcNow.Date);
                    break;
                case SortTimeEnum.Month:
                    query = query.Where(x => x.CreateAt.Month == DateTime.UtcNow.Month && x.CreateAt.Year == DateTime.UtcNow.Year);
                    break;  
                case SortTimeEnum.Year:
                    query = query.Where(x => x.CreateAt.Year == DateTime.Today.Year);
                    break;
                case SortTimeEnum.Week:
                    DateTime weekStart = DateTime.UtcNow.Date.AddDays(-DAYS_IN_WEEK);
                    query = query.Where(x => x.CreateAt >= weekStart);
                    break;
                case SortTimeEnum.HalfAYear:
                    DateTime halfYearStart = DateTime.UtcNow.Date.AddDays(-HALF_OF_YEAR);
                    query = query.Where(x => x.CreateAt >= halfYearStart);
                    break;
                case SortTimeEnum.AllTime:
                    break;
                default:
                    query = query.Where(x => x.CreateAt.Month == DateTime.Today.Month);
                    break;
            }

            if (model.Limit != 0)
            {
                query = query.Take(model.Limit);
            }
            else
            {
                query = query.Take(DEFAULT_LIMIT);  
            }
            
            // TODO учесть приватность более подробно
            query = query.Where(x => x.Private == false);
            
            
            return await query.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<ModuleEntity>> GetModulesShortInfo(int[] moduleIds, CancellationToken cancellationToken)
        {
            return await _context.Modules.Where(x => moduleIds.Contains(x.Id)).ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
