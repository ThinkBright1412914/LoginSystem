namespace LoginSystem.Client.Models
{
    public class ForgotPasswordConfirmVM
    {
        public Guid? Id { get; set; }    
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
