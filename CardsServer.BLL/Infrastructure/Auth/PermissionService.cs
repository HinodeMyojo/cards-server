using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsServer.BLL.Infrastructure.Auth
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _repository;

        public PermissionService(IPermissionRepository repository)
        {
            _repository = repository;
        }

        public async Task<HashSet<string>> GetPermissionsAsync(int userId)
        {
            RoleEntity[] roles = await _repository
                .GetPermissionsAsync(userId);

            return roles
                .SelectMany(x => x.Permissions)
                .Select(x => x.Title)
                .ToHashSet();
        }
    }
}
