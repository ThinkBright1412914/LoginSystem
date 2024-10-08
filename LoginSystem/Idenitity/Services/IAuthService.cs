﻿using LoginSystem.Domain.Model;
using LoginSystem.DTO;
using LoginSystem.Utility;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.IdentityModel.Tokens;
using NETCore.Encrypt;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using LoginSystem.CustomMapper;

namespace LoginSystem.Idenitity.Services
{
    public interface IAuthService
    {
        Task<AuthenticationDTO> Login(LoginDto request);

        Task<UserDataVM> Register(RegisterUser request);

		Task<UserDataVM> Activate(UserDataVM request);
    }

	public class AuthService : IAuthService
	{
		private readonly ApplicationDbContext _context;
		private readonly IEmailSender _emailSender;
		public IConfiguration _configuration;

		public AuthService(ApplicationDbContext context, IEmailSender emailSender, IConfiguration configuration)
		{
			_context = context;
			_emailSender = emailSender;
			_configuration = configuration;
		}


		public async Task<AuthenticationDTO> Login(LoginDto request)
		{
			try
			{
				AuthenticationDTO response = new AuthenticationDTO();
				request.Password = EncryptProvider.Base64Encrypt(request.Password);
				var user = await GetUser(request.UserName, request.Password);
				if (user != null)
				{
					if (user.IsActive)
					{
						if (!user.IsForcePasswordReset)
						{
							string imgData = user.ImageFile != null ? Convert.ToBase64String(user.ImageFile) : string.Empty;
							var userRole = _context.UserRoles?
													.Include(x => x.Roles)?
													.FirstOrDefault(x => x.UserId == user.UserId)?.Roles.RoleName;
							var claims = new List<Claim>
									{
										new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
										new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
										new Claim("UserId", user.UserId.ToString()),
										new Claim("UserName", user.UserName),
										new Claim("Email", user.Email),
										new Claim(ClaimTypes.Role, userRole)
									};

							var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
							var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
							var token = new JwtSecurityToken(
								_configuration["Jwt:Issuer"],
								_configuration["Jwt:Audience"],
								claims,
								expires: DateTime.Now.AddDays(7),
								signingCredentials: signIn);

							string tokenHandler = new JwtSecurityTokenHandler().WriteToken(token);

							response.Token = tokenHandler;
							var userDataVM = UserMapper.ToModel(user);
							userDataVM.ImageData = imgData;
							response.User = userDataVM;
							response.Message = "Success";
							return response;
						}
						else
						{
							UserDataVM userDataVM = new UserDataVM()
							{
								UserId = user.UserId,
								isForcePswdReset = user.IsForcePasswordReset,
								Email = user.Email,
								Password = user.Password,
							};
							response.User = userDataVM;
							response.Message = "Force password reset.";
							return response;
						}
					}
					else
					{
						var time = DateTime.Now.AddMinutes(5);
						var code = Security.GenerateActivationCode();
						await _emailSender.SendEmailAsync
							(
								user.Email,
								"Activation Code",
								$"Dear User,<br/>" +
								$"Your activation code is: {code}. It will expire in 5 minutes."
							);

						user.ExpirationDate = time;
						user.ActivationCode = code;
						_context.UserInfos.Update(user);
						await _context.SaveChangesAsync();

						var userDataVM = UserMapper.ToModel(user);
						userDataVM.ExpirationDate = time;
						userDataVM.ActivationCode = code;
						response.User = userDataVM;
						response.Message = "Inactive";
					}
				}
				else
				{
					response.Message = "NotFound";
				}


				return response;
			}
			catch (Exception ex)
			{
				throw ex;
			}


		}


		public async Task<UserDataVM> Register(RegisterUser model)
		{
			try
			{
				var dbContext = _context.RegisterUsers.AsEnumerable();
				dbContext = dbContext.Where(x => x.Email.Equals(model.Email, StringComparison.OrdinalIgnoreCase)).ToList(); ;
				UserDataVM response = new UserDataVM();
				if (dbContext.Any())
				{
					response.Message = "Email Already Exists.";
				}
				else
				{
					model.Password = EncryptProvider.Base64Encrypt(model.Password);
					model.ConfirmPassword = model.Password;
					Guid newUser = Guid.NewGuid();
					model.Id = newUser;

					var time = DateTime.Now.AddMinutes(5);
					var code = Security.GenerateActivationCode();

					UserInfo user = new UserInfo()
					{
						UserId = newUser,
						UserName = model.UserName,
						Email = model.Email,
						ActivationCode = code,
						Password = model.Password,
						ExpirationDate = time,
					};

					UserRole userRole = new UserRole()
					{
						UserId = user.UserId,
						RoleId = new Guid(UserConstant.UserRole),
					};

					await _emailSender.SendEmailAsync
							(
								model.Email,
								"Activation Code",
								$"Dear User,<br/>" +
								$"Your activation code is: {code}. It will expire in 5 minutes."
							);

					_context.RegisterUsers.Add(model);
					_context.UserInfos.Add(user);
					_context.UserRoles.Add(userRole);
					_context.SaveChanges();

					response = UserMapper.ToModel(user);
					return response;
				}

				return response;

			}
			catch (Exception e)
			{
				throw;
			}

		}

		public async Task<UserDataVM> Activate(UserDataVM request)
		{
			var result = _context.UserInfos.FirstOrDefault(x => x.UserId == request.UserId);
			if (result != null)
			{
				if (result.ActivationCode == request.ActivationCode)
				{
					if (DateTime.Now <= result.ExpirationDate)
					{
						result.IsActive = true;
						_context.UserInfos.Update(result);
						_context.SaveChanges();

						UserDataVM response = new UserDataVM()
						{
							IsActive = result.IsActive,
							UserId = result.UserId,
						};

						return response;
					}
					else
					{
						throw new Exception($"Activation code has expired."); ;
					}
				}

				else
				{
					throw new Exception($"Activation code do not match.");
				}

			}
			else
			{
				throw new Exception($"{request.UserName} was not found.");
			}
		}

		private async Task<UserInfo> GetUser(string email, string Password)
		{
			return await _context?.UserInfos?.FirstOrDefaultAsync(
						u =>
							u.Email.ToLower() == email.ToLower() ||
							u.UserName.ToLower() == email.ToLower() &&
							u.Password == Password);
		}
	}
}
