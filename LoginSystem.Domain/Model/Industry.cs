using System.ComponentModel.DataAnnotations;

namespace LoginSystem.Domain.Model
{
	public class Industry
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
	}
}
