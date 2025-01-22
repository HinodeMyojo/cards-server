using CardsServer.BLL.Entity;
using CardsServer.DAL.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CardsServer.DAL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<AvatarEntity> Avatars { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<StatusEntity> Statuses { get; set; }
        public DbSet<PermissionEntity> Permissions { get; set; }
        public DbSet<ModuleEntity> Modules { get; set; }
        public DbSet<ElementEntity> Elements { get; set; }
        public DbSet<ElementImageEntity> Images { get; set; }
        public DbSet<RefreshTokenEntity> RefreshTokenEntities { get; set; }
        public DbSet<LogsEntity> Logs { get; set; }
        public DbSet<ProfileEntity> ProfileEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            // Проверяем переменную окружения
            string? connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
            if (connectionString == null)
            {
                var message = "CONNECTION_STRING не найдена в переменных окружения.";
                throw new Exception(message);
            }
            Console.WriteLine(connectionString);
            optionsBuilder.UseNpgsql(connectionString);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new AvatarConfiguration());
            modelBuilder.ApplyConfiguration(new StatusConfiguration());
            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration());
            modelBuilder.ApplyConfiguration(new ModuleConfiguration());
            modelBuilder.ApplyConfiguration(new ElementConfiguration());
        }
    }   
}
