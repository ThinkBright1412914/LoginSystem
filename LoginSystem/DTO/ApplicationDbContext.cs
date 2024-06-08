using LoginSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace LoginSystem.DTO
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<RegisterUser> RegisterUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserInfo>().HasData(UserSeed.DefaultUser());
        }

    }
}
