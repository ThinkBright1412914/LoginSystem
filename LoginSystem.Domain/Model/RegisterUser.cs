using System.ComponentModel.DataAnnotations;

namespace LoginSystem.Domain.Model
{
    public class RegisterUser
    {
        [Key]
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
