using System.ComponentModel.DataAnnotations;


namespace LoginSystem.Domain.Model
{
	public class Cinema
	{
		[Key]
		public int Id { get; set; }	
		public string Name { get; set; }
		public string Location { get; set; }
		public string City { get; set; }	
	}
}
