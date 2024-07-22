using LoginSystem.ViewModel;

namespace LoginSystem.Idenitity.Services
{
    public interface IUserService
    {
        Task<UserDataVM> ResetPassword(UserDataVM request);

        Task<UserDataVM> ForgotPassword(ForgotPasswordDto request);

        bool ForgotPasswordConfirm(ForgotPasswordConfirmDto request);

		Task<UserDataVM> EditUser(UserDataVM request); 

        Task<UserDataVM> ForcePasswordReset(UserDataVM request);
	}
}
