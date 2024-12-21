using Microsoft.EntityFrameworkCore;
using AndroidCA_backend.Models;
namespace AndroidCA_backend.DAL
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserModel>().HasData(
                 new UserModel { Id = 1, Username = "testuser", Password = "password123", IsPaidUser = false },
                new UserModel { Id = 2, Username = "admin", Password = "admin123", IsPaidUser = true }
            );
        }
        public DbSet<UserModel> Users { get; set; }
    }
}
