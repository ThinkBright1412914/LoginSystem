namespace LoginSystem.Client.Models
{
    public class UserVM
    {
        public Guid? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }

        public string? Password { get; set; }

        public bool IsActive { get; set; } = false;

        public string? ActivationCode { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public string Message { get; set; }
    }
}
