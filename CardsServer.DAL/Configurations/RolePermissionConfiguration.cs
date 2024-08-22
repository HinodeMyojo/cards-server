using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardsServer.DAL.Configurations
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<Dictionary<int, int>>
    {
        public void Configure(EntityTypeBuilder<Dictionary<int, int>> builder)
        {
            builder.ToTable("RolesPermissions");

            builder.HasData(
            new { RoleId = 1, PermissionId = 4 },  // Пользователь - GetOwnObject
            new { RoleId = 1, PermissionId = 5 },  // Пользователь - CreateObject
            new { RoleId = 1, PermissionId = 6 },  // Пользователь - EditOwnObject
            new { RoleId = 1, PermissionId = 7 },  // Пользователь - DeleteOwnObject

            new { RoleId = 2, PermissionId = 1 },  // Администратор - EditAllObject
            new { RoleId = 2, PermissionId = 2 },  // Администратор - DeleteAllObject
            new { RoleId = 2, PermissionId = 3 },  // Администратор - GetAllObject
            new { RoleId = 2, PermissionId = 5 },  // Администратор - CreateObject

            new { RoleId = 3, PermissionId = 3 },  // Модератор - GetAllObject
            new { RoleId = 3, PermissionId = 1 },  // Модератор - EditAllObject
            new { RoleId = 3, PermissionId = 5 },  // Модератор - CreateObject
            new { RoleId = 3, PermissionId = 7 }   // Модератор - DeleteOwnObject
        );

        }
    }
}
