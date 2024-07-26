using LoginSystem.Domain.Model;

namespace LoginSystem.ViewModel
{
	public class MovieDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Image { get; set; }
		public string? ReleaseDate { get; set; }
		public string? Duration { get; set; }
		public int LanguageId { get; set; }
		public int GenreId { get; set; }
		public int IndustryId { get; set; }
		public string? Message { get;set; }

		public List<IndustryDto>? IndustryList { get; set; }
		public List<GenreDto>? GenresList { get; set; }	
		public List<LanguageDto>? LanguageList { get; set; }

	}
}
