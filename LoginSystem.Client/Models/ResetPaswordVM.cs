namespace LoginSystem.Client.Models
{
    public class ResetPaswordVM
    {
        public string Email { get; set; }   
        public string CurrentPassword { get;set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
