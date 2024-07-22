using LoginSystem.DTO;
using LoginSystem.Idenitity.Services;
using LoginSystem.Utility;
using LoginSystem.ViewModel;
using NETCore.Encrypt;

namespace LoginSystem.Idenitity
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserDataVM> ForgotPassword(ForgotPasswordDto request)
        {
            try
            {
                var userInfo =  _context.UserInfos.FirstOrDefault(x => x.Email.ToLower() == request.Email.ToLower());
                if (userInfo != null)
                {
                    UserDataVM response = new UserDataVM()
                    {
                        UserId = userInfo.UserId,
                        UserName = userInfo.UserName,
                        Email = userInfo.Email,
                        Password = userInfo.Password,
                        IsActive = userInfo.IsActive,
                        ExpirationDate = userInfo.ExpirationDate,
                        ActivationCode = userInfo.ActivationCode,
                        Message = "Success"
                    };
                       
                    return response;
                }
                else
                {
                    throw new Exception("Email not found");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserDataVM> ResetPassword(UserDataVM request)
        {
            try
            {
                var result = _context.UserInfos.FirstOrDefault(x => x.UserId == request.UserId);
                if (result != null)
                {
					result.Password = request.Password;
                    _context.UserInfos.Update(result);
                    _context.SaveChanges();

                    UserDataVM response = new()
                    {
                        UserId = result.UserId,
                        UserName = result.UserName,
                        Email = result.Email,
                        Password = result.Password,
                        IsActive = result.IsActive,
                        ExpirationDate = result.ExpirationDate,
                        ActivationCode = result.ActivationCode,
                        ImageData = result.ImageFile != null ? Convert.ToBase64String(result.ImageFile) : null
                    };
                    return response;
                }
                else
                {
                    throw new Exception("Current user not found.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserDataVM> ForcePasswordReset(UserDataVM request)
        {
            try
            {
                var result = _context.UserInfos.FirstOrDefault(x => x.UserId == request.UserId);
                if (result != null)
                {
                    result.Password = request.Password;
                    result.IsForcePasswordReset = false;
                    _context.UserInfos.Update(result);
                    _context.SaveChanges();

                    UserDataVM response = new()
                    {
                        Message = "Password sucessfully updated"
                    };
                    return response;
                }
                else
                {
                    throw new Exception("Current user not found.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        bool IUserService.ForgotPasswordConfirm(ForgotPasswordConfirmDto request)
        {
            try
            {
                var response = _context.UserInfos.FirstOrDefault(x => x.UserId == request.Id);
                if (response != null)
                {
                    request.Password = EncryptProvider.Base64Encrypt(request.Password);
                    response.Password = request.Password;
                    _context.UserInfos.Update(response);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

		public async Task<UserDataVM> EditUser(UserDataVM request)
		{
			try
			{
				var currentUser = _context.UserInfos.FirstOrDefault(x => x.UserId == request.UserId);
				if (currentUser != null)
				{
                    if(request.ImageData != null)
                    {
                        var imgData = new Converter().ConvertBase64ToByteArray(request.ImageData);
                        currentUser.ImageFile = imgData;
                    }                                    
                    currentUser.UserName = request.UserName;
                    currentUser.Email = request.Email;                   
					_context.UserInfos.Update(currentUser);
					_context.SaveChanges();
					UserDataVM response = new UserDataVM()
					{
						UserId = currentUser.UserId,
						Email = currentUser.Email,
						ExpirationDate = currentUser.ExpirationDate,
						ActivationCode = currentUser.ActivationCode,
						Password = currentUser.Password,
						IsActive = currentUser.IsActive,
						UserName = currentUser.UserName,
						Message = "Success",
						ImageData = request.ImageData,
					};
					return response;
				}
				else
				{
					throw new Exception("Current user not found.");
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
