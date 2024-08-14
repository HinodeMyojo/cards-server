using CardsServer.BLL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardsServer.DAL.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.HasData(
                new RoleEntity()
                {
                    Id = 1,
                    Name = "Пользователь",
                },
                new RoleEntity()
                {
                    Id = 2,
                    Name = "Администратор",
                },
                new RoleEntity()
                {   Id = 3,
                    Name = "Модератор",
                });
        }
    }
}
