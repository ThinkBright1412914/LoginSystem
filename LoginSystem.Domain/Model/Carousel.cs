using System.ComponentModel.DataAnnotations;

namespace LoginSystem.Domain.Model
{
	public class Carousel
	{
		[Key]
		public int Id { get; set; }

		public byte[]? Image { get; set; }
	}
}
