using LoginSystem.ViewModel;

namespace LoginSystem.Idenitity.Services
{
    public interface IRoles
    {
        Task<IEnumerable<RoleDTO>> GetRolesAsync();    
        Task<RoleDTO> CreateRoleAsync(RoleDTO role);
        Task<RoleDTO> DeleteRoleAsync(Guid roleId);
    }
}
