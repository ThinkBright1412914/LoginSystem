

namespace LoginSystem.Domain.Model
{
	public class Role
	{
		public Guid RoleId { get; set; }	

		public string RoleName { get; set; }

		public ICollection <UserInfo> Users { get; set; }
		public ICollection<UserRole> UsersRoles { get; set; }	
	}
}
