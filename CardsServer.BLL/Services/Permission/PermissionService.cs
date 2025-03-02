using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Entity;

namespace CardsServer.BLL.Services.Permission
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUserRepository _userRepository;
        public async Task<IEnumerable<string>> GetPermissions(int userId, CancellationToken cancellationToken)
        {
            UserEntity? user = await _userRepository.GetUserAsync(
                x => x.Id == userId, 
                cancellationToken);

            if (user == null)
            {
                throw new ArgumentNullException("User not found"); 
            }

            IEnumerable<PermissionEntity> result = await _permissionRepository.Get(user.RoleId, cancellationToken);
            
            return result.Select(x => $"{x.Title}").ToList();
        }
    }
}