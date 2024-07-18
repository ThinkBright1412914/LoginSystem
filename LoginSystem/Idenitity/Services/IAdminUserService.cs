using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Idenitity.Services
{
    public interface IAdminUserService
    {
        Task<List<UserDataVM>> GetUsers();
        Task<UserDataVM> GetUserById(Guid Id);
        Task<UserDataVM> CreateUser(UserDataVM request); 
        Task<UserDataVM> UpdateUser(UserDataVM request);
        Task<UserDataVM> DeleteUser(Guid id);
        Task<FileContentResult> GeneratePDf(UserDataVM request);
	}
}
