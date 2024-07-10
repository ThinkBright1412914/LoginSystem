using System.ComponentModel.DataAnnotations;

namespace LoginSystem.Client.Models
{
	public class RoleVM
	{
		public Guid? RoleId { get; set; }

		[Required]
		public string RoleName { get; set; }

		public string? Message { get; set; }
	}
}
