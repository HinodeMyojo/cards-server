using CardsServer.BLL.Entity;
using Microsoft.EntityFrameworkCore;
namespace CardsServer.DAL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }
    }
}
