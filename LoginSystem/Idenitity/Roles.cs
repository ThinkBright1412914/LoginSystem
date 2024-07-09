using LoginSystem.DTO;
using LoginSystem.Idenitity.Services;
using LoginSystem.Model;
using LoginSystem.ViewModel;

namespace LoginSystem.Idenitity
{
    public class Roles : IRoles
    {
        private readonly ApplicationDbContext _context;

        public Roles(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoleDTO>> GetRolesAsync()
        {
            try
            {
                List<RoleDTO> role = new();
                var response = _context.Roles.Select(x => new RoleDTO
                {
                    RoleId = x.RoleId,
                    RoleName = x.RoleName
                })
                .ToList();

                if (response != null)
                {
                    return response;
                }
                else
                {
                    return role;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<RoleDTO> CreateRoleAsync(RoleDTO model)
        {
            try
            {
                RoleDTO response = new();
                var query = _context.Roles.AsEnumerable();
                if(model.RoleName != null)
                {
                    query = query.Where(x => x.RoleName.Equals(model.RoleName, StringComparison.OrdinalIgnoreCase));
                    if(query.Any())
                    {
                        response.Message = "Role name already exist.";
                    }
                    else
                    {
                        Guid roleId = Guid.NewGuid();
                        Role newRole = new()
                        {
                            RoleId = roleId,
                            RoleName = model.RoleName
                        };
                        _context.Roles.Add(newRole);
                        _context.SaveChanges();
                        response.Message = "Created Successfully.";
                    } 
                    return response;
                }
                else
                {
                    response.Message = "Role required.";
                }
              
                return response;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<RoleDTO> DeleteRoleAsync(Guid roleId)
        {
            try
            {
                RoleDTO response = new();
                var result = await _context.Roles.FindAsync(roleId);
                if (result != null)
                {
                    _context.Roles.Remove(result);
                    _context.SaveChanges();
                    response.Message = "Deleted Successfully.";
                    return response;
                }
                else
                {
                    response.Message = "Role was not found.";
                    return response;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

       

    }
}
