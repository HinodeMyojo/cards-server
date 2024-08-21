using CardsServer.BLL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CardsServer.DAL.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<PermissionEntity>
    {
        public void Configure(EntityTypeBuilder<PermissionEntity> builder)
        {
            builder.HasData(
                new PermissionEntity()
                {
                    Id = 1,
                    Title = "EditAllObject",
                    Description = "Редактирование всех записей",
                },
                new PermissionEntity()
                {
                    Id = 2,
                    Title = "DeleteAllObject",
                    Description = "Удаление всех записей",
                },
                new PermissionEntity()
                {
                    Id = 3,
                    Title = "GetAllObject",
                    Description = "Удаление всех записей",
                },
                new PermissionEntity()
                {
                    Id = 4,
                    Title = "GetOwnObject",
                    Description = "Получение своих записей",
                },
                new PermissionEntity()
                {
                    Id = 5,
                    Title = "CreateOwnObject",
                    Description = "Создание своих записей",
                },
                new PermissionEntity()
                {
                    Id = 6,
                    Title = "EditOwnObject",
                    Description = "Редактирование своих записей",
                },
                new PermissionEntity()
                {
                    Id = 7,
                    Title = "DeleteOwnObject",
                    Description = "Удаление своих записей",
                });
        }
    }
}
