using LoginSystem.Model;

namespace LoginSystem.DTO
{
	public class RoleSeed
	{
		public static List<Role> DefaultRoleSeed()
		{
			List<Role> roles = new List<Role>()
			{
				new Role()
				{
					RoleId = new Guid("91fb10cd-b0ce-4e45-8763-6aaf1b8cb2f9"),
					RoleName = "Admin",
				},
				new Role()
				{
					RoleId = new Guid("115f896e-5a2a-4cf1-a90c-2bb3c01740dc"),
					RoleName = "User"
				}
			};
		return roles;
		}
	}
}
