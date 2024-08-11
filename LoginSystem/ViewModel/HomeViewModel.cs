namespace LoginSystem.ViewModel
{
	public class HomeViewModel
	{
		public List<CarouselDto> Carousels { get; set; }
		public List<MovieDto>? PremierMovieList { get; set; }
		public List<MovieDto>? UpcomingMovieList { get; set; }
	}
}
