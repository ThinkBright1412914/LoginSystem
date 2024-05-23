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
                    return View("ActivationCode");
                }

            }
            else
            {
                TempData["Error"] = "Please, Enter the code";
                return View();
            }
        }

        //public async Task<IActionResult> ActivationCode()
        //{
        //    return View();
        //}

    }

}
