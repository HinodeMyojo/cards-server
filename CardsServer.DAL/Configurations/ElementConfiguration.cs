using CardsServer.BLL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CardsServer.DAL.Configurations
{
    public class ElementConfiguration : IEntityTypeConfiguration<ElementEntity>
    {
        public void Configure(EntityTypeBuilder<ElementEntity> builder)
        {
            builder.HasKey(x => x.Id);

            // Связь с модулем 1:М
            builder.HasOne(x => x.Module)
                .WithMany(x => x.Elements)
                .HasForeignKey(x => x.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь 1:1 с изображением
            builder.HasOne(x => x.Image)
                .WithOne(x => x.Element)
                .HasForeignKey<ElementImageEntity>(x => x.ElementId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
