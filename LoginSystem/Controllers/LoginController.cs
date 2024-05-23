using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LoginSystem.DTO;
using LoginSystem.Model;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LoginSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public LoginController(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }



        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate(Login model)
        {
                var user = await GetUser(model.UserName, model.Password);
                if (user != null)
                {
                    var claims = new[]
                    {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                            new Claim("UserId", user.UserId.ToString()),
                            new Claim("Email" , user.Email)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }

                else
                {
                    return BadRequest("Invalid credentials");
                }

        }

        private bool isUserValid(string email, string Password)
        {
            if (email == "admin" && Password == "admin")
            {
                return true;
            }
            return false;
        }

        private async Task<UserInfo> GetUser(string email, string Password)
        {
            return await _context.UserInfos.FirstOrDefaultAsync(u => u.Email == email && u.Password == Password);
        }
    }
}
