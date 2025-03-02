using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Entity;

namespace CardsServer.BLL.Services.Permission
{
    public class PermissionService(IPermissionRepository permissionRepository, IUserRepository userRepository)
        : IPermissionService
    {
        public async Task<IEnumerable<string>> GetPermissions(int userId, CancellationToken cancellationToken)
        {
            UserEntity? user = await userRepository.GetUserAsync(
                x => x.Id == userId, 
                cancellationToken,
                x => x.UserPermissions);

            if (user == null)
            {
                throw new ArgumentNullException("User not found"); 
            }

            var userPermission = user.UserPermissions;
            
            int[] userPermissionIds = userPermission.Select(x => x.Id).ToArray();
            
            IEnumerable<PermissionEntity> userPermisssionsFromDb = await permissionRepository.Get(userPermissionIds, cancellationToken);
            
            IEnumerable<PermissionEntity> permissionsFromDb = await permissionRepository.GetByRoleId(user.RoleId, cancellationToken);

            ICollection<PermissionEntity> resultPermissions = [];

            
            foreach (var permission in permissionsFromDb)
            {
                if (userPermisssionsFromDb.Any(p => p.Id == permission.Id))
                {
                    UserPermissionEntity? userPerm = userPermission.FirstOrDefault(x => x.Id == permission.Id);

                    if (userPerm is not { isGranted: true })
                    {
                        continue; 
                    }
                }

                resultPermissions.Add(permission);
            }
            
            foreach (UserPermissionEntity userPerm in userPermission)
            {
                if (userPerm.isGranted && resultPermissions.All(p => p.Id != userPerm.PermissionId))
                {
                    PermissionEntity? permission = userPermisssionsFromDb.FirstOrDefault(p => p.Id == userPerm.Id);
                    if (permission is null)
                    {
                        continue;
                    }
                    resultPermissions.Add(permission);
                }
            }
            
            return resultPermissions.Select(x => $"{x.Title}").ToList();
        }
    }
}