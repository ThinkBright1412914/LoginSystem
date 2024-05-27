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
using NETCore.Encrypt;

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
		public async Task<IActionResult> Authenticate(LoginDto model)
		{
			try
			{
				model.Password = EncryptProvider.Base64Encrypt(model.Password);
				var user = await GetUser(model.UserName, model.Password);
				if (user != null)
				{
					var claims = new[]
					{
							new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
							new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
							new Claim("UserId", user.UserId.ToString()),
							new Claim("UserName" , user.UserName),
							new Claim("Email" , user.Email)
					};

					var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
					var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
					var token = new JwtSecurityToken(
						_configuration["Jwt:Issuer"],
						_configuration["Jwt:Audience"],
						claims,
						expires: DateTime.Now.AddDays(7),
						signingCredentials: signIn) ;

					string tokenHandler = new JwtSecurityTokenHandler().WriteToken(token);

					AuthenticationDTO obj = new AuthenticationDTO()
					{
						Token = tokenHandler,
						User = user,
					};
					return Ok(obj);
				}

				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}


		[HttpPut("ResetPassword")]
		public async Task<IActionResult> ResetPassword(UserInfo model)
		{
			try
			{
				var response = _context.UserInfos.FirstOrDefault(x => x.UserId == model.UserId);
				if (response != null)
				{
					response.Password = model.Password;
					_context.UserInfos.Update(response);
					_context.SaveChanges();
					return Ok(response);
				}
				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}


		[HttpPut("ForgotPassword")]
		public async Task<IActionResult> ForgotPassword(ForgotPasswordDto model)
		{
			try
			{
				var response = _context.UserInfos.FirstOrDefault(x => x.Email.ToLower() == model.Email.ToLower());
				if (response != null)
				{
					return Ok(response);
				}
				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		[HttpPut("ForgotPasswordConfirm")]
		public async Task<IActionResult> ForgotPasswordConfirm(ForgotPasswordConfirmDto model)
		{
			try
			{
				var response = _context.UserInfos.FirstOrDefault(x => x.UserId == model.Id);
				if (response != null)
				{
					model.Password = EncryptProvider.Base64Encrypt(model.Password);
					response.Password = model.Password;
					_context.UserInfos.Update(response);
					_context.SaveChanges();
					return Ok();
				}
				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		private async Task<UserInfo> GetUser(string email, string Password)
		{
			return await _context.UserInfos.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower() || u.UserName.ToLower() == email.ToLower() && u.Password == Password);
		}
	}
}
