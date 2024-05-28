using System.ComponentModel.DataAnnotations;

namespace LoginSystem.Client.Models
{
    public class RegisterVM
    {
		
        public string UserName { get; set; }

		[Required]
		[RegularExpression(@"^[a-zA-Z0-9-a-zA-Z0-9.!#$%&'*+-=?^_`{|}~\/]+@([-a-zA-Z0-9]+\.)+[a-zA-Z]{2,10}$",
ErrorMessage = "NotValidEmail")]
		public string Email { get; set; }

		[Required]
		[RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\\]*+\\/|!\"£$%^&*()#[@~'?><,.=_-]).{5,20}$",
			 ErrorMessage = "Atleast use 1 big alphabets 1 small alphabets 1 special character")]
		public string Password { get; set; }


		[Required]
		[Compare(nameof(Password), ErrorMessage = "Password do not match")]
		[RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\\]*+\\/|!\"£$%^&*()#[@~'?><,.=_-]).{5,20}$",
			 ErrorMessage = "Atleast use 1 big alphabets 1 small alphabets 1 special character")]
		public string ConfirmPassword { get; set; }
	}
}
