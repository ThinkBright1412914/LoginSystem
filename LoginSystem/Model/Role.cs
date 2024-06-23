using Org.BouncyCastle.Asn1.Mozilla;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace LoginSystem.Model
{
	public class Role
	{
		public Guid RoleId { get; set; }	

		public string RoleName { get; set; }

		public ICollection <UserInfo> Users { get; set; }
		public ICollection<UserRole> UsersRoles { get; set; }	
	}
}
