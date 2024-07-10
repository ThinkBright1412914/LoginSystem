using LoginSystem.Client.Models;

namespace LoginSystem.Client.Service.Interfaces
{
	public interface IRoleRequest
	{
		Task<List<RoleVM>> GetRoles();
		Task<RoleVM> CreateByAdminRole(RoleVM model);
		Task<RoleVM> DeleteByAdminRole(Guid Id);
	}
}
