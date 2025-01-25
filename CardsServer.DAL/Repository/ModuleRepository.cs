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

            switch (model.SortOption)
            {
                case SortOptionEnum.Newest:
                    query = query.OrderByDescending(x => x.CreateAt);
                    break;
                case SortOptionEnum.Oldest:
                    query = query.OrderBy(x => x.CreateAt);
                    break;
                case SortOptionEnum.Popularity:
                    query = query.OrderBy(x => x.UserModules.Count);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreateAt);
                    break;
            }

            switch (model.SortTime)
            {
                case SortTimeEnum.Day:
                    query = query.Where(x => x.CreateAt.DayOfYear == DateTime.Today.DayOfYear);
                    break;
                case SortTimeEnum.Month:
                    query = query.Where(x => x.CreateAt.Month == DateTime.Today.Month);
                    break;  
                case SortTimeEnum.Year:
                    query = query.Where(x => x.CreateAt.Year == DateTime.Today.Year);
                    break;
                case SortTimeEnum.Week:
                    query = query.Where(x => (x.CreateAt.ToUniversalTime() - DateTime.Today.ToUniversalTime()).Days <= DAYS_IN_WEEK);
                    break;
                case SortTimeEnum.HalfAYear:
                    query = query.Where(x => (x.CreateAt.ToUniversalTime() - DateTime.Today.ToUniversalTime()).Days <= HALF_OF_YEAR);
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
