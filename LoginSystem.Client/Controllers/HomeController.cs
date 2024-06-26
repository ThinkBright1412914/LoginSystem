﻿using DocumentFormat.OpenXml.Spreadsheet;
using LoginSystem.Client.Models;
using LoginSystem.Client.Service;
using LoginSystem.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace LoginSystem.Client.Controllers
{
    [Authorize] 
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SessionService _sessionService;
        private readonly UserService _userService;
        private readonly IHttpContextAccessor _context;

        public HomeController(ILogger<HomeController> logger, SessionService sessionService, 
            UserService userService , IHttpContextAccessor context)
        {
            _logger = logger;
            _sessionService = sessionService;
            _userService = userService;
            _context = context;
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

        [HttpGet]
		public async Task<IActionResult> EditUser()
		{
			var result = _sessionService.GetUserSession();
            UserVM model = new UserVM()
            {
                UserId = result.UserId,
                UserName = result.UserName,
                Email = result.Email,
                ActivationCode = result.ActivationCode,
                ExpirationDate = result.ExpirationDate,
                Message = "Request",
                ImageData = result.ImageData 
            };

            UserImageVM userVM = new UserImageVM()
            {
                User = model
            };
            return View(userVM);
		}

		[HttpPost]
		public async Task<IActionResult> EditUser(UserImageVM model)
		{
			if (model != null)
			{
                if(model.Image != null)
                {
					var img = await new Converter().ConvertToBase64(model.Image);
					model.User.ImageData = img;
				}
           
                var result = await _userService.EditUser(model.User);
				if (result != null)
				{
                    _sessionService.SetUserSession(result);
					return RedirectToAction("Index");

				}
			}
			return View(model);
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
