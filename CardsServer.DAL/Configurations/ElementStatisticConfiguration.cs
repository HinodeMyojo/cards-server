using CardsServer.BLL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardsServer.DAL.Configurations
{
    public class ElementStatisticConfiguration : IEntityTypeConfiguration<ElementStatisticEntity>
    {
        public void Configure(EntityTypeBuilder<ElementStatisticEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User)
                .WithMany(x => x.ElementStatistics)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Element)
                .WithMany(x => x.ElementStatistics)
                .HasForeignKey(x => x.ElementId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
