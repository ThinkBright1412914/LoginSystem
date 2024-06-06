﻿using LoginSystem.DTO;
using LoginSystem.Idenitity.Services;
using LoginSystem.Model;
using LoginSystem.ViewModel;
using NETCore.Encrypt;

namespace LoginSystem.Idenitity
{
    public class UserService : IUserServive
    {
        private readonly ApplicationDbContext _context;

        public UserService (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserInfo> ForgotPassword(ForgotPasswordDto request)
        {
            try
            {
                var response =  _context.UserInfos.FirstOrDefault(x => x.Email.ToLower() == request.Email.ToLower());
                if (response != null)
                {
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

        public async Task<UserInfo> ResetPassword(UserInfo request)
        {
            try
            {
                var response = _context.UserInfos.FirstOrDefault(x => x.UserId == request.UserId);
                if (response != null)
                {
                    response.Password = request.Password;
                    _context.UserInfos.Update(response);
                    _context.SaveChanges();
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

        bool IUserServive.ForgotPasswordConfirm(ForgotPasswordConfirmDto request)
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
    }
}