using LoginSystem.Client.Models;
using LoginSystem.Utility;
using Newtonsoft.Json;
using System.Text;

namespace LoginSystem.Client.Service
{
    public class UserService
    {
        private readonly IhttpService _httpService;

        public UserService(IhttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<AuthenticationModel> Login(LoginViewModel model)
        {
            string json = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var (status, response) = await _httpService.PostAsync<AuthenticationModel>(ApiUri.Login, content);
            return response;
        }

        public async Task<UserVM> Register(RegisterVM model)
        {
            string json = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var (status, response) = await _httpService.PostAsync<UserVM>(ApiUri.Reigster, content);
            return response;
        }

        public async Task<UserVM> ActivateCode(UserVM model)
        {
            string json = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var (status, response) = await _httpService.PostAsync<UserVM>(ApiUri.ActivateCode, content);
            return response;
        }

        public async Task<UserVM> ResetPassword(UserVM model)
        {
            string json = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var (status, response) = await _httpService.PutAsync<UserVM>(ApiUri.ResetPassword, content);
            return response;
        }

        public async Task<UserVM> ForgotPassword(ForgotPasswordVM model)
        {
            string json = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var (status, response) = await _httpService.PutAsync<UserVM>(ApiUri.ForgotPassword, content);
            return response;
        }

        public async Task<UserVM> ForgotPasswordConfirm(ForgotPasswordConfirmVM model)
        {
            string json = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var (status, response) = await _httpService.PutAsync<UserVM>(ApiUri.ForgotPasswordConfirm, content);
            return response;
        }

        public async Task<UserVM> EditUser(UserVM model)
        {
            string json = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var (status, response) = await _httpService.PutAsync<UserVM>(ApiUri.EditUser, content);
            return response;
        }

        public async Task<UserVM> ForcePasswordReset(UserVM model)
        {
            string json = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var (status, response) = await _httpService.PutAsync<UserVM>(ApiUri.ForcePasswordReset, content);
            return response;
        }


        // Admin Section
        public async Task<List<UserVM>> GetUsers()
        {
            var (status, response) = await _httpService.GetAsync<List<UserVM>>(ApiUri.GetUsers);
            return response;
        }

        public async Task<UserVM> GetUserById(Guid Id)
        {
            var (status, response) = await _httpService.GetAsync<UserVM>(ApiUri.GetUserById + "?Id=" + Id);
            return response;
        }

        public async Task<UserVM> CreateByAdminUser(UserVM model)
        {
            string json = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var (status, response) = await _httpService.PostAsync<UserVM>(ApiUri.CreateUser, content);
            return response;
        }

        public async Task<UserVM> UpdateByAdminUser(UserVM model)
        {
            string json = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var (status, response) = await _httpService.PutAsync<UserVM>(ApiUri.UpdateUser, content);
            return response;
        }

        public async Task<UserVM> DeleteByAdminUser(UserVM model)
        {
            var (status, response) = await _httpService.DeleteAsync<UserVM>(ApiUri.DeleteUser + "?Id=" + model.UserId);
            return response;
        }

    }
}
