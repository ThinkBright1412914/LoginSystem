using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginSystem.Domain.Model
{
	public class Show
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }

		[ForeignKey(nameof(Movie))]
		public int MovieId { get; set; }

		public DateTime ShowDate { get; set; }

		[ForeignKey(nameof(ShowTime))]
		public int ShowTimeId { get; set; }
		public string? SeatNo { get;set; }
		public decimal TicketPrice {  get; set; }	

		public ShowTime ShowTimeInfo { get; set; }	
		public Movie MovieInfo { get; set; }	
	}
}
