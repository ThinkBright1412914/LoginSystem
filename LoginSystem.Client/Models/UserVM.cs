using System.ComponentModel.DataAnnotations;

namespace LoginSystem.Client.Models
{
    public class UserVM
    {
        public Guid UserId { get; set; }

        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Email { get; set; }

        public string? Password { get; set; }

        public bool IsActive { get; set; } = false;

        public string? ActivationCode { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public string? Message { get; set; }

        public string? ImageData { get; set; }

        public string? Role { get; set; }

        public bool isForcePswdReset { get; set; }


    }
}
