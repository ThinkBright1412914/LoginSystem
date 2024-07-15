

using System.ComponentModel.DataAnnotations.Schema;

namespace LoginSystem.Domain.Model
{
	public class Movie
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public byte[]? Image { get; set; }
		public DateTime ReleaseDate {get;set;}
		public string Duration { get; set;}

		[ForeignKey(nameof(Language))]
		public int LanguageId { get;set; }

		[ForeignKey(nameof(Genre))]
		public int GenreId { get; set; }

		[ForeignKey(nameof(Industry))]
		public int IndustryId { get;set; }

		public Language Language { get; set; }
		public Genre Genre { get; set; }
		public Industry Industry { get; set; }	
	}
}
