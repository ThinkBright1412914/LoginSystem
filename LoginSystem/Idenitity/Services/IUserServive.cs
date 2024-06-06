using LoginSystem.Model;
using LoginSystem.ViewModel;

namespace LoginSystem.Idenitity.Services
{
    public interface IUserServive
    {
        Task<UserInfo> ResetPassword(UserInfo request);

        Task<UserInfo> ForgotPassword(ForgotPasswordDto request);

        bool ForgotPasswordConfirm(ForgotPasswordConfirmDto request);
    }
}
