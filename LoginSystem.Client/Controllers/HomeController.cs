using LoginSystem.Client.Models;
using LoginSystem.Client.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LoginSystem.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SessionService _sessionService;

        public HomeController(ILogger<HomeController> logger, SessionService sessionService)
        {
            _logger = logger;
            _sessionService = sessionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ProfileInfo()
        {
            var userInfo = _sessionService.GetUserSession();
            return View(userInfo);
        }

        public IActionResult LogOut()
        {
            _sessionService.LogOut();
            return RedirectToAction("Index", "Authenticate");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
