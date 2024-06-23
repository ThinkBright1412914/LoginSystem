using LoginSystem.Model;
using LoginSystem.ViewModel;

namespace LoginSystem.Idenitity.Services
{
    public interface IUserServive
    {
        Task<UserDataVM> ResetPassword(UserDataVM request);

        Task<UserDataVM> ForgotPassword(ForgotPasswordDto request);

        bool ForgotPasswordConfirm(ForgotPasswordConfirmDto request);

		Task<UserDataVM> EditUser(UserDataVM request); 
	}
}
