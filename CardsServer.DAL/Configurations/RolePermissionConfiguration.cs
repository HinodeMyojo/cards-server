using CardsServer.BLL.Entity;
using CardsServer.BLL.Enums;
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
                Create(RoleEnum.User, PermissionEnum.CanViewModule),
                Create(RoleEnum.User, PermissionEnum.CanStudyModule),
                Create(RoleEnum.User, PermissionEnum.CanEditOwnModule),
                Create(RoleEnum.User, PermissionEnum.CanDeleteOwnModule),
                Create(RoleEnum.User, PermissionEnum.CanEditOwnProfile),
                Create(RoleEnum.User, PermissionEnum.CanViewOwnProfile),

                // Роль Moderator
                Create(RoleEnum.Moderator, PermissionEnum.CanViewAnyModule),
                Create(RoleEnum.Moderator, PermissionEnum.CanBlockModule),
                Create(RoleEnum.Moderator, PermissionEnum.CanBlockUser),

                // Роль Admin
                Create(RoleEnum.Admin, PermissionEnum.CanAddModule),
                Create(RoleEnum.Admin, PermissionEnum.CanCreateModule),
                Create(RoleEnum.Admin, PermissionEnum.CanEditAnyModule),
                Create(RoleEnum.Admin, PermissionEnum.CanDeleteAnyModule),
                Create(RoleEnum.Admin, PermissionEnum.CanDeleteUser),
                Create(RoleEnum.Admin, PermissionEnum.CanEditAnyProfile),
                Create(RoleEnum.Admin, PermissionEnum.CanViewAnyProfile)
            );
        }
        private static RolePermissionEntity Create(RoleEnum roleEnum, PermissionEnum permission)
        {
            return new RolePermissionEntity
            {
                RoleId = (int)roleEnum,
                PermissionId = (int)permission
            };
        }
    }
}
