using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Entity;
using Microsoft.EntityFrameworkCore;

namespace CardsServer.DAL.Repository
{
    public sealed class PermissionRepository(ApplicationContext context) : IPermissionRepository
    {
        public async Task<IEnumerable<PermissionEntity>> GetByRoleId(int roleId, CancellationToken cancellationToken)
        {
            return await context.Permissions
                .Where(x => x.RolePermissions
                    .Any(entity => entity.RoleId == roleId))
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<PermissionEntity>> Get(int[] userPermissionIds, CancellationToken cancellationToken)
        {
            return await context.Permissions
                .Where(x => userPermissionIds
                    .Contains(x.Id))
                .ToListAsync(cancellationToken);
        }
    }
}