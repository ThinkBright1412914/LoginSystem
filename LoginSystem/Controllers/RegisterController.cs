
using LoginSystem.DTO;
using LoginSystem.Model;
using LoginSystem.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt;
using Org.BouncyCastle.Ocsp;
using System.Net;


namespace LoginSystem.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RegisterController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly IEmailSender _emailSender;

		public RegisterController(ApplicationDbContext context, IEmailSender emailSender)
		{
			_context = context;
			_emailSender = emailSender;
		}


		[HttpPost("SignUp")]
		public async Task<IActionResult> Register(RegisterUser model)
		{
			try
			{
				model.Password = EncryptProvider.Base64Encrypt(model.Password);
				model.ConfirmPassword = model.Password;
				Guid refId = Guid.NewGuid();
				model.Id = refId;

				var time = DateTime.Now.AddMinutes(5);
				var code = Security.GenerateActivationCode();

				UserInfo user = new UserInfo()
				{
					UserId = refId,
					UserName = model.UserName,
					Email = model.Email,
					ActivationCode = code,
					Password = model.Password,
					ExpirationDate = time,
				};

				_emailSender.SendEmailAsync(model.Email, "Activate Code", $"Dear User , Your activation code is {code}." +
					$"It will expire in 5 minutes");

				_context.RegisterUsers.Add(model);
				_context.UserInfos.Add(user);
				_context.SaveChanges();
				return Ok(user);

			}
			catch (Exception ex)
			{
				throw ex;
			}

		}


		[HttpPost("ActivationCode")]
		public async Task<IActionResult> ActivationCode(UserInfo model)
		{
			var getUserById = _context.UserInfos.FirstOrDefault(x => x.UserId == model.UserId);
			if (getUserById != null)
			{
				if (getUserById.ActivationCode == model.ActivationCode)
				{
					if (DateTime.Now <= getUserById.ExpirationDate)
					{
						getUserById.IsActive = true;
						_context.UserInfos.Update(getUserById);
						_context.SaveChanges();
						return Ok(getUserById);
					}
					else
					{
						return NotFound();
					}
				}

				else
				{
					return NotFound();
				}

			}
			else
			{
				return BadRequest();
			}
		}

		[HttpPut("ResendActivationCode")]
		public async Task<IActionResult> ResendActivationCode(UserInfo model)
		{
			var getUserById = _context.UserInfos.FirstOrDefault(x => x.UserId == model.UserId);
			if (getUserById != null)
			{
				var time = DateTime.Now.AddMinutes(5);
				var code = Security.GenerateActivationCode();

				UserInfo user = new UserInfo()
				{
					ActivationCode = code,
					ExpirationDate = time,
				};

				_emailSender.SendEmailAsync(getUserById.Email, "Activate Code", $"Dear User , Your activation code is {code}." +
					$"It will expire in 5 minutes");

				_context.UserInfos.Update(user);
				_context.SaveChanges();
				return Ok();

			}
			else
			{
				return BadRequest();
			}
		}

	}
}
