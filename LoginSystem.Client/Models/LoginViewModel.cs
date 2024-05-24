using System.ComponentModel.DataAnnotations;

namespace LoginSystem.Client.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Enter your username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        public string Password { get; set; }    
    }
}
