using LoginSystem.DTO;
using LoginSystem.Model;
using LoginSystem.Utility;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                List<UserDataVM> user = new();
                var result = _context.UserInfos.ToList();   
                foreach (var item in result)
                {
                    UserDataVM userVM = new UserDataVM()
                    {
                        UserName = item.UserName,
                        Email = item.Email,
                        IsActive = item.IsActive,
                        ImageData = item.ImageFile != null ? Convert.ToBase64String(item.ImageFile) : null,
                    };
                    user.Add(userVM);
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
