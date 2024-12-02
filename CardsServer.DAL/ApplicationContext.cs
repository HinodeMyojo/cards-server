using CardsServer.BLL.Entity;
using CardsServer.DAL.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace CardsServer.DAL
{
    public class ApplicationContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<AvatarEntity> Avatars { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<StatusEntity> Statuses { get; set; }
        public DbSet<PermissionEntity> Permissions { get; set; }
        public DbSet<ModuleEntity> Modules { get; set; }
        public DbSet<ElementEntity> Elements { get; set; }
        public DbSet<ElementImageEntity> Images { get; set; }
        //public DbSet<ElementStatisticEntity> ElementStatistics { get; set; }
        public DbSet<RefreshTokenEntity> RefreshTokenEntities { get; set; }
        public ApplicationContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
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
            //modelBuilder.ApplyConfiguration(new ElementStatisticConfiguration());
        }
    }
}
