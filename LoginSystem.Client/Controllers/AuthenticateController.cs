using LoginSystem.Client.Models;
using LoginSystem.Client.Service;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Client.Controllers
{
    public class AuthenticateController : Controller
    {
        private readonly UserService _authService;
        private readonly SessionService _sessionService;

        public AuthenticateController(UserService authService , SessionService sessionService)
        {
            _authService = authService;
            _sessionService = sessionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            var response = await _authService.Login(model);
            if (response != null)
            {
                _sessionService.SetUserSession(response.User);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index");
            }

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
            if(model.ActivationCode != null || model.ActivationCode != string.Empty )
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
                    return RedirectToAction("Index", "Home");
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

        public async Task<IActionResult> ResetPassword()
        {
            return View();
        }


        [HttpPut]
        public async Task<IActionResult> ResetPassword(ResetPaswordVM model)
        {
            if(model != null)
            {
                var currentUserInfo = _sessionService.GetUserSession();
                if (currentUserInfo != null)
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
        public async Task<IActionResult> ForgotPassword(UserVM model)
        {
            if(model.Email != null)
            {
                var result = await _authService.ForgotPassword(model);
                if(result != null)
                {
                    _sessionService.SetUserSession(result);
                    return View(result);
                    
                }
            }
            return View(model.Email);
        }
    }

}
