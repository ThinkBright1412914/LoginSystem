using System.ComponentModel.DataAnnotations;

namespace LoginSystem.Domain.Model
{
	public class ShowTime
	{
		[Key]
		public int Id { get; set; }
		public string Time { get; set; }
	}
}
