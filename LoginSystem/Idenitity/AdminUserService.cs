using LoginSystem.DTO;
using LoginSystem.Idenitity.Services;
using LoginSystem.Model;
using LoginSystem.Utility;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using NETCore.Encrypt;


namespace LoginSystem.Idenitity
{
    public class AdminUserService : IAdminUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public AdminUserService(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        public async Task<List<UserDataVM>> GetUsers()
        {          
            List<UserDataVM> user = new();
            var result = _context.UserInfos.Include(x => x.UserRoles)
                                           .ThenInclude(x => x.Roles).ToList();
            foreach (var item in result)
            {
                UserDataVM userVM = new UserDataVM()
                {
                    UserId = item.UserId,
                    UserName = item.UserName,
                    Email = item.Email,
                    IsActive = item.IsActive,
                    ImageData = item.ImageFile != null ? Convert.ToBase64String(item.ImageFile) : null,
                    Role = item.UserRoles.FirstOrDefault().Roles.RoleName,
                    isForcePswdReset = item.IsForcePasswordReset,
                };
                user.Add(userVM);
            }
            return user;
        }

        public async Task<UserDataVM> GetUserById(Guid Id)
        {
            UserDataVM response = new();
            try
            {
                var result = _context.UserInfos
                            .Include(x => x.UserRoles)
                            .FirstOrDefault(x => x.UserId == Id);

                var userRole = _context.UserRoles.Include(x => x.Roles).FirstOrDefault(x => x.UserId == Id)?.Roles.RoleName;

                if (result != null)
                {
                    response.UserId = result.UserId;
                    response.UserName = result.UserName;
                    response.Role = userRole;
                    response.Email = result.Email;
                    response.ImageData = result.ImageFile != null ? Convert.ToBase64String(result.ImageFile) : "";
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return response;

        }

        public async Task<UserDataVM> CreateUser(UserDataVM request)
        {
            UserDataVM response = new();
            try
            {
                UserRole userRole = new();
                Guid Id = Guid.NewGuid();
                string pswd = Security.GenerateRandomPassword();
                var cypherPswd = EncryptProvider.Base64Encrypt(pswd);
                RegisterUser register = new()
                {
                    Id = Id,
                    UserName = request.UserName,
                    Password = cypherPswd,
                    ConfirmPassword = cypherPswd,
                    Email = request.Email
                };

                UserInfo user = new()
                {
                    UserId = Id,
                    Email = request.Email,
                    Password = cypherPswd,
                    UserName = request.UserName,
                };

                if (request.Role == "Admin")
                {
                    userRole.UserId = user.UserId;
                    userRole.RoleId = new Guid(UserConstant.AdminRole);
                }
                else
                {
                    userRole.UserId = user.UserId;
                    userRole.RoleId = new Guid(UserConstant.UserRole);
                }

                var createdDate = DateTime.Now;
                await _emailSender.SendEmailAsync
                    (
                        request.Email,
                        "Account Created",
                        $"Dear User,<br/>" +
                        $"Your account has been successfully created at {createdDate.ToString("yyyy MMMM dd dddd hh:mm tt")}. Please change your password to activate the account by using the provided information on the link below.<br/><br/>" +
                        $"Username: {request.UserName}<br/>" +
                        $"Password: {pswd}<br/><br/>"
                    );

                _context.RegisterUsers.Add(register);
                _context.UserInfos.Add(user);
                _context.UserRoles.Add(userRole);
                _context.SaveChanges();

                response.Message = "Account was successfully created.";
            }
            catch (Exception e)
            {
                throw;
            }

            return response;
        }

        public async Task<UserDataVM> UpdateUser(UserDataVM request)
        {
            UserDataVM response = new();

            try
            {
                var user = _context.UserInfos
                                          .Include(x => x.UserRoles)
                                          .ThenInclude(x => x.Roles)
                                          .FirstOrDefault(x => x.UserId == request.UserId);

                var role = _context.UserRoles.FirstOrDefault(x => x.UserId == request.UserId);

                if (role != null)
                {
                    user.UserRoles.Remove(role);
                }

                UserRole userRole = new();
                if (request.Role == "Admin")
                {
                    userRole.UserId = user.UserId;
                    userRole.RoleId = new Guid(UserConstant.AdminRole);
                }
                else
                {
                    userRole.UserId = user.UserId;
                    userRole.RoleId = new Guid(UserConstant.UserRole);
                }

                user.IsForcePasswordReset = request.isForcePswdReset;

                response.Message = "Updated Successfully";
                _context.UserRoles.Add(userRole);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw;
            }
            return response;
        }

        public async Task<UserDataVM> DeleteUser(Guid Id)
        {
            UserDataVM response = new();
            try
            {
                var user = _context.UserInfos.Include(x => x.UserRoles)
                                             .ThenInclude(x => x.Roles)
                                             .FirstOrDefault(x => x.UserId == Id);
                if (user != null)
                {
                    _context.Remove(user);
                    _context.SaveChanges();
                    response.Message = "User Deleted Succesfully.";
                }
                else
                {
                    response.Message = "User not found.";
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return response;
        }


    }
}
