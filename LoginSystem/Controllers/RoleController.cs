using LoginSystem.Idenitity.Services;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly IRoles _role;

        public RoleController(IRoles role)
        {
            _role = role;
        }

        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRole()
        {
            var response = await _role.GetRolesAsync();
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }


        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateUser(RoleDTO request)
        {
            var response = await _role.CreateRoleAsync(request);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpDelete("DeleteRole")]
        public async Task<IActionResult> DeleteUser(Guid Id)
        {
            var response = await _role.DeleteRoleAsync(Id);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
    }
}
