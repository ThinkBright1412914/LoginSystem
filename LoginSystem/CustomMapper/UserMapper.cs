using LoginSystem.Domain.Model;
using LoginSystem.ViewModel;

namespace LoginSystem.CustomMapper
{
	public static class UserMapper
	{
		public static UserDataVM ToModel(UserInfo dbUser)
		{
			UserDataVM model = new UserDataVM();
			model.UserId = dbUser.UserId;
			model.UserName = dbUser.UserName;
			model.Email = dbUser.Email;
			model.ExpirationDate = dbUser.ExpirationDate;
			model.ActivationCode = dbUser.ActivationCode;
			model.Password = dbUser.Password;
			model.IsActive = dbUser.IsActive;
			model.UserName = dbUser.UserName;
			model.Message = "Success";
			return model;
		}
	}
}
