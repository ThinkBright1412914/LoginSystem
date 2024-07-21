

namespace LoginSystem.ViewModel
{
	public class MovieDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Image { get; set; }
		public DateTime ReleaseDate { get; set; }
		public string? Duration { get; set; }
		public int LanguageId { get; set; }
		public int GenreId { get; set; }
		public int IndustryId { get; set; }
	}
}
