using CardsServer.BLL.Entity;
using CardsServer.BLL.Infrastructure.Auth.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardsServer.DAL.Configurations
{
    public sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermissionEntity>
    {
        public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
        {
            builder.HasKey(rp => new { rp.RoleId, rp.PermissionId });

            builder.HasData(
                Create(Role.User, Permission.ReadObjects),
                Create(Role.User, Permission.CreateObjects)
                );
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
