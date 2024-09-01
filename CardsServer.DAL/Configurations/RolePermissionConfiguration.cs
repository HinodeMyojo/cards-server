using CardsServer.BLL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardsServer.DAL.Configurations
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermissionEntity>
    {
        public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
        {
            builder.HasKey(rp => new { rp.RoleId, rp.PermissionId });
            builder.ToTable("RolesPermissions");

            builder.HasData(
            new RolePermissionEntity { RoleId = 1, PermissionId = 4 },  // Пользователь - GetOwnObject
            new RolePermissionEntity { RoleId = 1, PermissionId = 5 },  // Пользователь - CreateObject
            new RolePermissionEntity { RoleId = 1, PermissionId = 6 },  // Пользователь - EditOwnObject
            new RolePermissionEntity { RoleId = 1, PermissionId = 7 },  // Пользователь - DeleteOwnObject

            new RolePermissionEntity { RoleId = 2, PermissionId = 1 },  // Администратор - EditAllObject
            new RolePermissionEntity { RoleId = 2, PermissionId = 2 },  // Администратор - DeleteAllObject
            new RolePermissionEntity { RoleId = 2, PermissionId = 3 },  // Администратор - GetAllObject
            new RolePermissionEntity { RoleId = 2, PermissionId = 5 },  // Администратор - CreateObject

            new RolePermissionEntity { RoleId = 3, PermissionId = 3 },  // Модератор - GetAllObject
            new RolePermissionEntity { RoleId = 3, PermissionId = 1 },  // Модератор - EditAllObject
            new RolePermissionEntity { RoleId = 3, PermissionId = 5 },  // Модератор - CreateObject
            new RolePermissionEntity { RoleId = 3, PermissionId = 7 }   // Модератор - DeleteOwnObject
        );

        }
    }
}
