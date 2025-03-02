using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Entity;

namespace CardsServer.DAL.Repository
{
    public sealed class PermissionRepository(ApplicationContext context) : IPermissionRepository
    {
        private readonly ApplicationContext _context = context;
        public Task<IEnumerable<PermissionEntity>> Get(int userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}