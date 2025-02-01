using CardsServer.BLL.Entity;
using CardsServer.BLL.Enums;
using CardsServer.BLL.Infrastructure.Auth.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CardsServer.DAL.Configurations
{
    public sealed class PermissionConfiguration : IEntityTypeConfiguration<PermissionEntity>
    {
        public void Configure(EntityTypeBuilder<PermissionEntity> builder)
        {
            builder.ToTable(TableNames.Permissions.ToString());

            builder.HasKey(x => x.Id);

            IEnumerable<PermissionEntity> permissions = Enum
                .GetValues<PermissionEnum>()
                .Select(p => new PermissionEntity
                {
                    Id = (int)p,
                    Title = p.ToString(),
                });

            builder.HasData(permissions);
        }
    }
}
