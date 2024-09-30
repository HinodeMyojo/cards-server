using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Entity;

namespace CardsServer.BLL.Infrastructure.Auth
{
    public class PolicyService : IPolicyService
    {
        private readonly IPolicyRepository _repository;

        public PolicyService(IPolicyRepository repository)
        {
            _repository = repository;
        }

        public async Task<HashSet<string>> GetPermissionsAsync(int userId)
        {
            RoleEntity[] roles = await _repository
                .GetRolesAsync(userId);

            return roles
                .SelectMany(x => x.Permissions)
                .Select(x => x.Title)
                .ToHashSet();
        }

        public async Task<HashSet<string>> GetRolesAsync(int userId)
        {
            RoleEntity[] roles = await _repository
                .GetRolesAsync(userId);

            return roles.Select(x => x.Name).ToHashSet();
        }
    }
}
