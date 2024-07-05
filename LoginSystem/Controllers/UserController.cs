using LoginSystem.Idenitity.Services;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IAdminUserService _adminUserService;

        public UserController(IAdminUserService adminUserService)
        {
            _adminUserService = adminUserService;
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var response = await _adminUserService.GetUsers();
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUsersById(Guid Id)
        {
            var response = await _adminUserService.GetUserById(Id);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(UserDataVM request)
        {
            var response = await _adminUserService.CreateUser(request);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }


        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserDataVM request)
        {
            var response = await _adminUserService.UpdateUser(request);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }


        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(Guid Id)
        {
            var response = await _adminUserService.DeleteUser(Id);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

    }
}
