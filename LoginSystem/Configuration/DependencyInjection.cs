using LoginSystem.Idenitity.Services;
using LoginSystem.Idenitity;
using LoginSystem.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LoginSystem.Configuration
{
    public static class DependencyInjection
	{
		public static IServiceCollection AddServerServices(this IServiceCollection services)
		{
			services.AddScoped<IShow, Shows>();
            services.AddScoped<IShowTime, ShowTimes>();
            services.AddScoped<ICinemas, Cinemas>();
			services.AddScoped<IMovies, Movies>();
			services.AddScoped<ILanguage, Languages>();
			services.AddScoped<IGenre, Genres>();
			services.AddScoped<IIndustry, Industrys>();
			services.AddScoped<ICarousel, Carousels>();
			services.AddScoped<IRoles, Roles>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IAuthService, AuthService>();
			services.AddSingleton<IEmailSender, EmailSender>();
			services.AddScoped<IAdminUserService, AdminUserService>();
			return services;
		}

		public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services ,
			IConfiguration configuration)
		{
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
			{
				options.RequireHttpsMetadata = false;
				options.SaveToken = true;
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidAudience = configuration["Jwt:Audience"],
					ValidIssuer = configuration["Jwt:Issuer"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
				};
			});
			return services;
		}
	}
}
