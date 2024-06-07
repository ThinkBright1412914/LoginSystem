﻿using LoginSystem.Model;
using LoginSystem.ViewModel;

namespace LoginSystem.Idenitity.Services
{
    public interface IAuthService
    {
        Task<AuthenticationDTO> Login(LoginDto request);

        Task<UserDataVM> Register(RegisterUser request);

        Task<UserInfo> Activate(UserInfo request);

     
    }
}
