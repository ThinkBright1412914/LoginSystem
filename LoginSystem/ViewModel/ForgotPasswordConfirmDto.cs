namespace LoginSystem.ViewModel
{
    public class ForgotPasswordConfirmDto
    {
        public Guid Id { get; set; }    
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
