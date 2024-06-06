using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using LoginSystem.DTO;
using LoginSystem.Idenitity.Services;
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
		private readonly IAuthService _authService;
		private readonly IUserServive _userService;

		public LoginController(IAuthService authService , IUserServive userServive)
		{
			_authService = authService;
			_userService = userServive;
		}


		[HttpPost("Authenticate")]
		public async Task<IActionResult> Authenticate(LoginDto model)
		{
			var response = await _authService.Login(model);
			if(response != null)
			{
				return Ok(response);
			}
			return NotFound("Login failed.");

		}


		[HttpPut("ResetPassword")]
		public async Task<IActionResult> ResetPassword(UserInfo model)
		{

            var response = await _userService.ResetPassword(model);
            if (response != null)
            {
                return Ok(response);
            }
            return NotFound("Reset Password failed.");
        }


		[HttpPut("ForgotPassword")]
		public async Task<IActionResult> ForgotPassword(ForgotPasswordDto model)
		{
            var response = _userService.ForgotPassword(model);
            if (response.Result != null)
            {
                return Ok(response.Result);
            }
            return NotFound("Process aborted.");

        }

		[HttpPut("ForgotPasswordConfirm")]
		public async Task<IActionResult> ForgotPasswordConfirm(ForgotPasswordConfirmDto model)
		{
            var response = _userService.ForgotPasswordConfirm(model);
            if (response == true)
            {
                return Ok();
            }
            return NotFound("Process aborted.");
        }
	}
}
