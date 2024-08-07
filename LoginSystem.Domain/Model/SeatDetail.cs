using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginSystem.Domain.Model
{
	public class SeatDetail
	{
		[Key]
		public int Id { get; set; }

		[ForeignKey(nameof(UserInfo))]
		public Guid UserId { get; set; }

		[ForeignKey(nameof(Show))]
		public int ShowId { get; set;}
		public string? SeatNo { get; set;}

		public UserInfo User { get; set; }
		public Show ShowInfo { get; set; }
	}
}
