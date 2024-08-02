using LoginSystem.Domain.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        [ValidateNever]
        public IEnumerable<SelectListItem> IndustryList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>  GenresList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> LanguageList { get; set; }

	}
}
