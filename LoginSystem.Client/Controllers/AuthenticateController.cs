using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ClosedXML.Excel;
using LoginSystem.Client.Models;
using LoginSystem.Client.Service;
using LoginSystem.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
					if (response.User.isForcePswdReset)
					{
						_sessionService.SetUserSession(response.User);
						return RedirectToAction("ForcePasswordReset");
					}
					if (response.Token != null && response.User != null && response.Message == "Success")
					{
						_sessionService.SetUserSession(response.User);
						_sessionService.SetAuthenticationSession(response.Token);
						var tokenContent = _tokenHandler.ReadJwtToken(response.Token);
						var claims = ParseClaims(tokenContent);
						var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
						var login = _httpContextAccessor?.HttpContext?.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
						return RedirectToAction("Index", "Home");
					}
					else if(response.User != null && response.Message == "Inactive")
					{
                        _sessionService.SetUserSession(response.User);
                        return RedirectToAction("ActivationCode");
                    }
					else
					{
						TempData["error"] = "Incorrect Username and Password";
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
		public  IActionResult Edit(UserVM model)
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
			if (ModelState.IsValid)
			{
				var response = await _authService.Register(model);
				if (response.Message == "Success")
				{
					_sessionService.SetUserSession(response);
					TempData["success"] = "User has been registered.";
					return RedirectToAction("ActivationCode", "Authenticate");
				}
				else 
				{
					TempData["error"] = "Email Already Exists.";
                    return RedirectToAction("Register");
				}
				
			}
			else
			{
				return View(model);
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
					TempData["success"] = "Your code has been successfully activated.";
					return RedirectToAction("Index", "Authenticate");
				}
				else
				{
					TempData["error"] = "Invalid Code / Expired Code";
					return View("ActivationCode");
				}

			}
			else
			{
				TempData["error"] = "Please, Enter the code";
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
			if (ModelState.IsValid)
			{
				var currentUserInfo = _sessionService.GetUserSession();
				model.CurrentPassword = EncryptProvider.Base64Encrypt(model.CurrentPassword);
				if (currentUserInfo != null)
				{

					if (currentUserInfo.Password != model.CurrentPassword)
					{						
						TempData["error"] = "Your current password do not match.";
						return View();
					}
					else
					{
						model.Password = EncryptProvider.Base64Encrypt(model.Password);
                        currentUserInfo.Password = model.Password;
						var resetPassword = _authService.ResetPassword(currentUserInfo);
						if (resetPassword.Result != null)
						{
							_sessionService.SetUserSession(resetPassword.Result);
							TempData["success"] = "Password sucessfully updated";
							return RedirectToAction("Index" , "Home");
						}
						else
						{
							TempData["error"] = "Password update aborted.";
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
			if (ModelState.IsValid)
			{
				var result = await _authService.ForgotPassword(model);
				if (result.UserId != null)
				{
					_sessionService.SetUserSession(result);
					return RedirectToAction("ForgotPasswordConfirm");
				}
				else
				{
                    TempData["error"] = "Email doesnot exists.";
                    return View(model);					
				}
			}
			else
			{
                return View(model);
            }				
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
			if (ModelState.IsValid)
			{
				var result = await _authService.ForgotPasswordConfirm(model);
				if (result.Message == "Success")
				{
					TempData["success"] = "Your password changed successfully.";
					return RedirectToAction("Index", "Authenticate");
				}
				else
				{
					TempData["error"] = "Your password update aborted.";
					return NotFound();
				}
			}
			else
			{
				return View(model);
			}
			
		}

        public async Task<IActionResult> ForcePasswordReset()
        {
			var currentUserInfo = _sessionService.GetUserSession();
			ResetPaswordVM model = new()
			{
				Id = currentUserInfo.UserId
			};
			return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> ForcePasswordReset(ResetPaswordVM model)
        {
            if (ModelState.IsValid)
            {
                var currentUserInfo = _sessionService.GetUserSession();
                model.CurrentPassword = EncryptProvider.Base64Encrypt(model.CurrentPassword);
                model.Password = EncryptProvider.Base64Encrypt(model.Password);
                if (currentUserInfo != null)
                {
                    if (currentUserInfo.Password != model.CurrentPassword)
                    {
                        TempData["error"] = "Your current password do not match.";
                        return View();
                    }
					else if(currentUserInfo.Password == model.Password)
					{
                        TempData["error"] = "Please do not enter the same password. Try different this time.";
                        return View();
                    }
                    else
                    {
                        currentUserInfo.Password = model.Password;
                        var resetPassword = _authService.ResetPassword(currentUserInfo);
                        if (resetPassword.Result != null)
                        {
                            TempData["success"] = "Password sucessfully updated";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["error"] = "Password update aborted.";
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

        private IList<Claim> ParseClaims(JwtSecurityToken tokenContent)
		{
			var claims = tokenContent.Claims.ToList();
			return claims;
		}


        public async Task<IActionResult> ExportExcel()
        {
            try
            {
                List<UserVM> data = await  _authService.GetUsers();
                if (data != null || data.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(new Converter().ConvertToDataTable(data.ToList()));
                        using (MemoryStream stream = new MemoryStream())
                        {
                            string fileName = $"TryExcel_{DateTime.Now.ToString("dd/mm/yyyy")}.xlsx";
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocuments.spreadsheetml.sheet", fileName);
                        }
                    }

                }
            }
            catch (Exception e)
            {
                throw;
            }
            return RedirectToAction("Index");

        }


        
    }

}
