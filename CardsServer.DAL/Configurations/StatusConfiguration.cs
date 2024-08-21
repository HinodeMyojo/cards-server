using CardsServer.BLL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardsServer.DAL.Configurations
{
    public class StatusConfiguration : IEntityTypeConfiguration<StatusEntity>
    {
        public void Configure(EntityTypeBuilder<StatusEntity> builder)
        {
            builder
                .HasMany(x => x.Users)
                .WithOne(x => x.Status)
                .HasForeignKey(x => x.StatusId)
                .IsRequired();

            builder.HasData(new StatusEntity()
            {
                Id = 1,
                Title = "Действует",
            },
            new StatusEntity()
            {
                Id = 2,
                Title = "Заблокирован",
            },
            new StatusEntity()
            {
                Id = 3,
                Title = "Удален",
            });
        }
    }
}
