
using LoginSystem.DTO;
using LoginSystem.Idenitity.Services;
using LoginSystem.Model;
using LoginSystem.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt;
using System.Net;


namespace LoginSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IAuthService _authService;

        public RegisterController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> Register(RegisterUser model)
        {
            var response = _authService.Register(model);
            if (response.Result != null)
            {
                return Ok(response.Result);
            }
            return NotFound("Register interrupted due to some error");

        }


        [HttpPost("ActivationCode")]
        public async Task<IActionResult> ActivationCode(UserInfo model)
        {
            var response = _authService.Activate(model);
            if(response.Result != null)
            {
                return Ok(response.Result);
            }
            return NotFound("Code was not activated");
        }

    }
}
