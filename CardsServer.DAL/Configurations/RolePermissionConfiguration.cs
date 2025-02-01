using CardsServer.BLL.Entity;
using CardsServer.BLL.Enums;
using CardsServer.BLL.Infrastructure.Auth.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardsServer.DAL.Configurations
{
    public sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermissionEntity>
    {
        public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
        {
            // Устанавливаем составной первичный ключ
            builder.HasKey(rp => new { rp.RoleId, rp.PermissionId });

            // Заполняем данные
            builder.HasData(
                // Роль User
                Create(Role.User, PermissionEnum.CanViewModule),
                Create(Role.User, PermissionEnum.CanStudyModule),
                Create(Role.User, PermissionEnum.CanEditOwnModule),
                Create(Role.User, PermissionEnum.CanDeleteOwnModule),
                Create(Role.User, PermissionEnum.CanEditOwnProfile),
                Create(Role.User, PermissionEnum.CanViewOwnProfile),

                // Роль Moderator
                Create(Role.Moderator, PermissionEnum.CanViewAnyModule),
                Create(Role.Moderator, PermissionEnum.CanBlockModule),
                Create(Role.Moderator, PermissionEnum.CanBlockUser),

                // Роль Admin
                Create(Role.Admin, PermissionEnum.CanAddModule),
                Create(Role.Admin, PermissionEnum.CanCreateModule),
                Create(Role.Admin, PermissionEnum.CanEditAnyModule),
                Create(Role.Admin, PermissionEnum.CanDeleteAnyModule),
                Create(Role.Admin, PermissionEnum.CanDeleteUser),
                Create(Role.Admin, PermissionEnum.CanEditAnyProfile),
                Create(Role.Admin, PermissionEnum.CanViewAnyProfile)
            );
        }
        private static RolePermissionEntity Create(Role role, PermissionEnum permission)
        {
            return new RolePermissionEntity
            {
                RoleId = (int)role,
                PermissionId = (int)permission
            };
        }

        private static RolePermissionEntity Create(
            Role role, Permission permission)
        {
            return new RolePermissionEntity
            {
                RoleId = (int)role,
                PermissionId = (int)permission
            };
        }
    }
}
