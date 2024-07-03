using LoginSystem.ViewModel;

namespace LoginSystem.Idenitity.Services
{
    public interface IAdminUserService
    {
        Task<List<UserDataVM>> GetUsers();
        Task<UserDataVM> GetUserById(Guid Id);
        Task<UserDataVM> CreateUser(UserDataVM request); 
        Task<UserDataVM> UpdateUser(UserDataVM request);
        Task<UserDataVM> DeleteUser(Guid id);
    }
}
