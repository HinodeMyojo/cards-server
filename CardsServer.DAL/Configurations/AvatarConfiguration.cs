using CardsServer.BLL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardsServer.DAL.Configurations
{
    public sealed class AvatarConfiguration : IEntityTypeConfiguration<AvatarEntity>
    {
        public void Configure(EntityTypeBuilder<AvatarEntity> builder)
        {
            builder.HasData(
                new AvatarEntity()
                {
                    Id = 1,
                    AvatarUrl = "/images/avatar/base_avatar.jpg"
                });
        }
        
    }
}
