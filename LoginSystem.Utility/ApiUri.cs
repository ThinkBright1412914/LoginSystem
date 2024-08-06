namespace LoginSystem.Utility
{
	public static class ApiUri
	{

		public const string ApiVersion = "/api/";

		public const string Login = ApiVersion + "Login/Authenticate";

		public const string Reigster = ApiVersion + "Register/SignUp";

		public const string ActivateCode = ApiVersion + "Register/ActivationCode";

		public const string ResetPassword = ApiVersion + "Login/ResetPassword";

		public const string ForcePasswordReset = ApiVersion + "Login/ForcePasswordReset";

		public const string ForgotPassword = ApiVersion + "Login/ForgotPassword";

		public const string ForgotPasswordConfirm = ApiVersion + "Login/ForgotPasswordConfirm";

		public const string EditUser = ApiVersion + "Login/EditUser";

		public const string GetUsers = ApiVersion + "User/GetUsers";

		public const string CreateUser = ApiVersion + "User/CreateUser";

		public const string UpdateUser = ApiVersion + "User/UpdateUser";

		public const string DeleteUser = ApiVersion + "User/DeleteUser";

		public const string GetUserById = ApiVersion + "User/GetUserById";

		public const string CreateRole = ApiVersion + "Role/CreateRole";

		public const string GetRoles = ApiVersion + "Role/GetRoles";

		public const string DeleteRole = ApiVersion + "Role/DeleteRole";

		public const string GetCarousels = ApiVersion + "CarouselApi/GetCarousels";

		public const string CreateCarousel = ApiVersion + "CarouselApi/CreateCarousel";

		public const string DeleteCarousel = ApiVersion + "CarouselApi/DeleteCarousel";

		public const string GetIndustrys = ApiVersion + "IndustryApi/GetIndustry";

		public const string CreateIndustry = ApiVersion + "IndustryApi/CreateIndustry";

		public const string DeleteIndustry = ApiVersion + "IndustryApi/DeleteIndustry";

		public const string GetLanguages = ApiVersion + "LanguageApi/GetLanguages";

		public const string CreateLanguage = ApiVersion + "LanguageApi/CreateLanguage";

		public const string DeleteLanguage = ApiVersion + "LanguageApi/DeleteLanguage";

		public const string GetGenres = ApiVersion + "GenreApi/GetGenres";

		public const string CreateGenre = ApiVersion + "GenreApi/CreateGenre";

		public const string DeleteGenre = ApiVersion + "GenreApi/DeleteGenre";

		public const string GetMovies = ApiVersion + "MovieApi/GetMovies";

		public const string GetMovieById = ApiVersion + "MovieApi/GetMovieById";

		public const string CreateMovie = ApiVersion + "MovieApi/Create-Movie";

		public const string UpdateMovie = ApiVersion + "MovieApi/Update-Movie";

		public const string DeleteMovie = ApiVersion + "MovieApi/Delete-Movie";

		public const string GetCinema = ApiVersion + "CinemaApi/GetCinemas";

		public const string GetCinemaById = ApiVersion + "CinemaApi/GetCinemaById";

		public const string CreateCinema = ApiVersion + "CinemaApi/Create-Cinema";

		public const string UpdateCinema = ApiVersion + "CinemaApi/Update-Cinema";

		public const string DeleteCinema = ApiVersion + "CinemaApi/Delete-Cinema";

		public const string GetShowTime = ApiVersion + "ShowTimeApi/GetShowTime";

		public const string CreateShowTime = ApiVersion + "ShowTimeApi/Create-ShowTime";

		public const string DeleteShowTime = ApiVersion + "ShowTimeApi/Delete-ShowTime";


    }
}
