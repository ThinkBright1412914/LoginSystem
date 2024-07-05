
namespace LoginSystem.Model
{
	public class UserRole
	{
		public Guid UserId { get; set; }

		public UserInfo Users { get; set; }

		public Guid RoleId { get; set; }

		public Role Roles { get; set; }

	}
}
