using CardsServer.BLL.Entity;
using CardsServer.BLL.Infrastructure.Auth.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardsServer.DAL.Configurations
{
    public sealed class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.ToTable(TableNames.Roles.ToString());

            builder.HasKey(x => x.Id);

            builder
            .HasMany(role => role.Permissions) // RoleEntity имеет много Permissions
            .WithMany(permission => permission.Roles) // PermissionEntity имеет много Roles
            .UsingEntity<RolePermissionEntity>(
                j => j
                    .HasOne(rolePermission => rolePermission.Permission) // RolePermissionEntity ссылается на PermissionEntity
                    .WithMany(permission => permission.RolePermissions) // PermissionEntity имеет много RolePermissionEntity
                    .HasForeignKey(rolePermission => rolePermission.PermissionId),
                j => j
                    .HasOne(rolePermission => rolePermission.Role) // RolePermissionEntity ссылается на RoleEntity
                    .WithMany(role => role.RolePermissions) // RoleEntity имеет много RolePermissionEntity
                    .HasForeignKey(rolePermission => rolePermission.RoleId),
                j =>
                {
                    j.HasKey(t => new { t.RoleId, t.PermissionId });
                    j.ToTable(TableNames.RolePermissions.ToString());
                });

            IEnumerable<RoleEntity> roles = Enum
                .GetValues<Role>()
                .Select(p => new RoleEntity
                {
                    Id = (int)p,
                    Name = p.ToString(),
                });

            builder.HasData(roles);
        }
    }
}
