using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Entity;

namespace CardsServer.DAL.Repository
{
    public sealed class PermissionReposotory(ApplicationContext context) : IPermissionReposotory
    {
        private readonly ApplicationContext _context = context;

        public async Task<IEnumerable<PermissionEntity>> Get(int userId, CancellationToken cancellationToken)
        {
        }
    }
}