using LoginSystem.DTO;
using LoginSystem.Model;
using LoginSystem.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt;


namespace LoginSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;  

        public RegisterController(ApplicationDbContext context , IEmailSender emailSender)
        {
            _context = context; 
            _emailSender = emailSender;
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterUser model)
        {
            if (ModelState.IsValid)
            {
                model.Password = EncryptProvider.Base64Encrypt(model.Password);
                model.ConfirmPassword = model.Password;
                Guid userId = Guid.NewGuid();
                Guid regId = Guid.NewGuid();
                model.Id = regId;


                var time = DateTime.Now.AddMinutes(5) ;
                UserInfo user = new UserInfo()
                {
                    UserId = userId,
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = model.Password,
                    ExpirationDate = time,
                };

                var code = Security.GenerateActivationCode();           
                _emailSender.SendEmailAsync(model.Email, "Activate Code", $"Dear User , Your activation code is {code}." +
                    $"It will expire in 5 minutes");

                _context.RegisterUsers.Add(model);
                _context.UserInfos.Add(user);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }

            return BadRequest();
        }

    }
}
