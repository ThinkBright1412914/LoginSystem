using LoginSystem.Client.Service.Interfaces;
using LoginSystem.Client.Service;
using LoginSystem.Client.ViewComponents;

namespace LoginSystem.Client.ClientSetting
{
	public static class DependencyInjectionClient
	{
		public static IServiceCollection AddClientServices(this IServiceCollection services)
		{
			services.AddScoped<IBookingRequest, BookingRequest>();
			services.AddScoped<IShowRequest, ShowRequest>();	
			services.AddScoped<IShowTimeRequest, ShowTimeRequest>();
			services.AddScoped<ILanguageRequest, LanguageRequest>();
			services.AddScoped<IIndustryRequest, IndustryRequest>();
			services.AddScoped<IGenreRequest, GenreRequest>();
			services.AddScoped<IRoleRequest, RoleRequest>();
			services.AddScoped<ICarouselRequest, CarouselRequest>();
			services.AddScoped<IMovieRequest, MovieRequest>();	
			services.AddScoped<IhttpService, HttpService>();
			services.AddScoped<UserService>();
			services.AddScoped<SessionService>();	
			services.AddScoped<UserProfileViewComponent>();
			return services;
		}
	}
}
