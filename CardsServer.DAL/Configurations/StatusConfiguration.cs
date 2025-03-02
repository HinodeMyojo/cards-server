using CardsServer.BLL.Entity;
using CardsServer.BLL.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardsServer.DAL.Configurations
{
    public sealed class StatusConfiguration : IEntityTypeConfiguration<StatusEntity>
    {
        public void Configure(EntityTypeBuilder<StatusEntity> builder)
        {
            builder
                .HasMany(x => x.Users)
                .WithOne(x => x.Status)
                .HasForeignKey(x => x.StatusId)
                .IsRequired();

            IEnumerable<StatusEntity> roles = Enum
                .GetValues<StatusEnum>()
                .Select(p => new StatusEntity
                {
                    Id = (int)p,
                    Title = p.ToString(),
                });

            builder.HasData(roles);
        }
    }
}
