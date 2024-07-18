using LoginSystem.ViewModel;

namespace LoginSystem.Client.Service.Interfaces
{
	public interface IRoleRequest
	{
		Task<List<RoleDTO>> GetRoles();
		Task<RoleDTO> CreateByAdminRole(RoleDTO model);
		Task<RoleDTO> DeleteByAdminRole(Guid Id);
	}
}
