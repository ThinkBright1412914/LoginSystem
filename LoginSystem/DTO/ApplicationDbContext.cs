using LoginSystem.Domain.Model;
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
		public DbSet<UserRole> UserRoles { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Carousel> Carousels { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Language> Languages { get; set; }
		public DbSet<Industry> Industries { get; set; }
		public DbSet<Movie> Movies { get; set; }
		public DbSet<Show> Shows { get; set; }
		public DbSet<ShowTime> ShowTime { get; set; }
		public DbSet<Booking> Bookings { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<UserRole>()
				.HasKey(u => new { u.UserId, u.RoleId });
			modelBuilder.Entity<UserRole>()
				.HasOne(u => u.Users)
				.WithMany(u => u.UserRoles)
				.HasForeignKey(u => u.UserId);
			modelBuilder.Entity<UserRole>()
				.HasOne(u => u.Roles)
				.WithMany(u => u.UsersRoles)
				.HasForeignKey(u => u.RoleId);

			modelBuilder.Entity<Booking>()
			   .HasOne(b => b.User)
			   .WithMany()
			   .HasForeignKey(b => b.UserId)
			   .OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Booking>()
				.HasOne(b => b.ShowInfo)
				.WithMany()
				.HasForeignKey(b => b.ShowId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Show>()
				.HasOne(s => s.MovieInfo)
				.WithMany()
				.HasForeignKey(s => s.MovieId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Show>()
				.HasOne(s => s.ShowTimeInfo)
				.WithMany()
				.HasForeignKey(s => s.ShowTimeId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<UserInfo>().HasData(UserSeed.DefaultUser());
			modelBuilder.Entity<Role>().HasData(RoleSeed.DefaultRoleSeed());
			modelBuilder.Entity<UserRole>().HasData(UserSeed.DefaultUserRole());
		}

	}
}

