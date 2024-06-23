using LoginSystem.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;

namespace LoginSystem.DTO
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<RegisterUser> RegisterUsers { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }  
        public DbSet<Role> Roles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>()
                .HasKey(u => new { u.UserId , u.RoleId });
            modelBuilder.Entity<UserRole>()
                .HasOne(u => u.Users)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(u => u.UserId);
            modelBuilder.Entity<UserRole>()
                .HasOne(u => u.Roles)
                .WithMany(u => u.UsersRoles)
                .HasForeignKey(u => u.RoleId);

			modelBuilder.Entity<UserInfo>().HasData(UserSeed.DefaultUser());
            modelBuilder.Entity<Role>().HasData(RoleSeed.DefaultRoleSeed());
        }

    }
}
