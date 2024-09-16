namespace LoginSystem.ViewModel
{
    public class UserDataVM
    {
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }

        public string? Password { get; set; }

        public bool IsActive { get; set; }

        public string? ActivationCode { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public string? Message { get; set; }

        public string? ImageData { get;set; }

        public bool isForcePswdReset { get; set; }

        public string? Role { get; set; }
    }
}
