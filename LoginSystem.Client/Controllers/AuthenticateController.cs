using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LoginSystem.Client.Models;
using LoginSystem.Client.Service;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NETCore.Encrypt;

namespace LoginSystem.Client.Controllers
{
	public class AuthenticateController : Controller
	{
		private readonly UserService _authService;
		private readonly SessionService _sessionService;
		private JwtSecurityTokenHandler _tokenHandler;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public AuthenticateController(UserService authService, SessionService sessionService , IHttpContextAccessor httpContextAccessor)
		{
			_authService = authService;
			_sessionService = sessionService;
			_tokenHandler = new JwtSecurityTokenHandler();
			_httpContextAccessor = httpContextAccessor;
		}


		public async Task<IActionResult> GetUsers()
		{
			List<UserVM> users = await _authService.GetUsers();
			return View(users);
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(LoginViewModel model)
		{
			try
			{
				if (ModelState.IsValid)
				{

					var response = await _authService.Login(model);
					if (response.Token != null && response.User != null)
					{
						_sessionService.SetUserSession(response.User);
						_sessionService.SetAuthenticationSession(response.Token);
						var tokenContent = _tokenHandler.ReadJwtToken(response.Token);
						var claims = ParseClaims(tokenContent);
						var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
						var login = _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
						return RedirectToAction("Index", "Home");
					}
					else
					{
						TempData["Error"] = "Incorrect Username and Password";
						return RedirectToAction("Index");
					}
				}
				else
				{
					return View(model);
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}

			
		}

		public IActionResult Edit()
		{
			var editUserInfo = _sessionService.GetUserSession();
			return View(editUserInfo);
		}

		[HttpPost]
		public IActionResult Edit(UserVM model)
		{
			var editUserInfo = _sessionService.GetUserSession();
			return View(editUserInfo);
		}

		public IActionResult Register()
		{
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> Register(RegisterVM model)
		{
			var response = await _authService.Register(model);
			if (response != null)
			{
				_sessionService.SetUserSession(response);
				return RedirectToAction("ActivationCode", "Authenticate");
			}
			else
			{
				return RedirectToAction("Index");
			}

		}


		public async Task<IActionResult> ActivationCode()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ActivationCode(UserVM model)
		{
			if (model.ActivationCode != null)
			{
				var userInfo = _sessionService.GetUserSession();
				UserVM obj = new UserVM()
				{
					UserId = userInfo.UserId,
					ActivationCode = model.ActivationCode.Trim(),
				};
				var response = await _authService.ActivateCode(obj);
				if (response.IsActive)
				{
					TempData["Error"] = "Your code has been successfully activated.";
					return RedirectToAction("Index", "Authenticate");
				}
				else
				{
					TempData["Error"] = "Invalid Code / Expired Code";
					return View("ActivationCode");
				}

			}
			else
			{
				TempData["Error"] = "Please, Enter the code";
				return View();
			}
		}

		[Authorize]
		public async Task<IActionResult> ResetPassword()
		{
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPaswordVM model)
		{
			if (model != null)
			{
				var currentUserInfo = _sessionService.GetUserSession();
				model.Password = EncryptProvider.Base64Encrypt(model.Password);
				if (currentUserInfo != null)
				{

					if (currentUserInfo.Password != model.Password)
					{
						TempData["error"] = "Your current password do not match.";
						return View();
					}
					else
					{
						currentUserInfo.Password = model.Password;
						var resetPassword = _authService.ResetPassword(currentUserInfo);
						if (resetPassword != null)
						{
							TempData["msg"] = "Password sucessfully updated";
							return RedirectToAction("Index", "Home");
						}
						else
						{
							TempData["msg"] = "Password update aborted.";
							return View();
						}
					}

				}
				else
				{
					return NotFound();
				}
			}
			else
			{
				return View(model);
			}
		}


		public async Task<IActionResult> ForgotPassword()
		{
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
		{
			if (model.Email != null)
			{
				var result = await _authService.ForgotPassword(model);
				if (result != null)
				{
					_sessionService.SetUserSession(result);
					return RedirectToAction("ForgotPasswordConfirm");

				}
			}
			return View(model.Email);
		}


		public async Task<IActionResult> ForgotPasswordConfirm()
		{
			var result = _sessionService.GetUserSession();
			ForgotPasswordConfirmVM model = new ForgotPasswordConfirmVM()
			{
				Id = result.UserId,
			};
			return View(model);
		}


		[HttpPost]
		public async Task<IActionResult> ForgotPasswordConfirm(ForgotPasswordConfirmVM model)
		{
			if (model != null)
			{
				var result = await _authService.ForgotPasswordConfirm(model);
				if (result == null)
				{
					TempData["Error"] = "Your password changed successfully.";
					return RedirectToAction("Index", "Authenticate");

				}
			}
			return View(model);
		}


		private IList<Claim> ParseClaims(JwtSecurityToken tokenContent)
		{
			var claims = tokenContent.Claims.ToList();
			return claims;
		}
	}

}
