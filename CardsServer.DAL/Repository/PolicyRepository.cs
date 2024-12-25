using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Entity;
using Microsoft.EntityFrameworkCore;

namespace CardsServer.DAL.Repository
{
    public sealed class PolicyRepository : IPolicyRepository
    {
        private readonly ApplicationContext _context;

        public PolicyRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<RoleEntity[]> GetRolesAsync(int userId)
        {
            return await _context.Set<UserEntity>()
                .Include(x => x.Role)
                .ThenInclude(x => x.Permissions)
                .Where(x => x.Id == userId)
                .Select(x => x.Role)
                .ToArrayAsync();

        }
    }
}
