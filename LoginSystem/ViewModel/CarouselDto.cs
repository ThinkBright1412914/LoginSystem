namespace LoginSystem.ViewModel
{
    public class CarouselDto
    {
        public int? Id { get; set; } 
        public string? Image { get; set; }

        public string? Message { get; set; }

        public List<MovieDto>? PremierMovieList { get; set; }

        public List<MovieDto>? UpcomingMovieList { get; set; }
    }
}
