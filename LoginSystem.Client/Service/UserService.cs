﻿using LoginSystem.Client.Models;
using LoginSystem.Utility;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;

namespace LoginSystem.Client.Service
{
    public class UserService
    {
        private readonly IhttpService _httpService;

        public UserService (IhttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<LoginViewModel> Login(LoginViewModel model)
        {
            string json = JsonConvert.SerializeObject(model);
            StringContent content =  new StringContent(json, Encoding.UTF8 , "application/json");
            var (status ,response) = await _httpService.PostAsync<LoginViewModel>(ApiUri.Login, content);
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
    }
}
