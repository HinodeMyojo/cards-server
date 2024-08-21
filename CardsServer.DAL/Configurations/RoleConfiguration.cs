using CardsServer.BLL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardsServer.DAL.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder
                .HasMany(x => x.Users)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId)
                .IsRequired();

            builder
                .HasMany(x => x.Permissions)
                .WithMany(x => x.Roles)
                .UsingEntity( y => y.ToTable("RolesPermissions"));


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
