﻿using LoginSystem.Domain.Model;
using LoginSystem.DTO;
using LoginSystem.Utility;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt;
using PdfSharpCore.Pdf;
using PdfSharpCore;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using Microsoft.EntityFrameworkCore;

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
					Role = item.UserRoles?.FirstOrDefault()?.Roles.RoleName,
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

				var userRole = _context.UserRoles.Include(x => x.Roles)
												 .FirstOrDefault(x => x.UserId == Id)?.Roles.RoleName;

				if (result != null)
				{
					response.UserId = result.UserId;
					response.UserName = result.UserName;
					response.Role = userRole;
					response.Email = result.Email;
					response.ImageData = result.ImageFile != null ? Convert.ToBase64String(result.ImageFile) : "";
					response.isForcePswdReset = result.IsForcePasswordReset;
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
						$"Your account has been successfully created at {createdDate.ToString("yyyy MMMM dd dddd hh:mm tt")}. Please activate the account by using the provided information on the link below.<br/><br/>" +
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
				var user = await _context.UserInfos
										 .Include(x => x.UserRoles)
										 .FirstOrDefaultAsync(x => x.UserId == request.UserId);

				if (user == null)
				{
					response.Message = "User not found";
					return response;
				}

				var existingUserRole = user.UserRoles.FirstOrDefault();
				if (existingUserRole != null)
				{
					_context.UserRoles.Remove(existingUserRole);
				}

				UserRole newUserRole = new UserRole
				{
					UserId = user.UserId,
					RoleId = request.Role == "Admin" ? new Guid(UserConstant.AdminRole) : new Guid(UserConstant.UserRole)
				};

				user.UserRoles.Add(newUserRole);
				user.IsForcePasswordReset = request.isForcePswdReset;
				_context.UserInfos.Update(user);

				await _context.SaveChangesAsync();

				response.Message = "Updated Successfully";
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

		public async Task<FileContentResult> GeneratePDf(UserDataVM request)
		{
			var document = new PdfDocument();
			Random random = new Random();
			int invoiceNo = random.Next(0, 10);
			var currentDate = DateTime.Now;
			string htmlContent = $@"
                        <html lang=""en"">
                        <head>
                        <meta charset=""UTF-8"">
                        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                        <title>UserInfo</title>
                        <style>
                          body {{
                            font-family: Arial, sans-serif;
                            line-height: 1.6;
                            background-color: #f0f0f0;
                            padding: 20px;
                          }}
                          .invoice-container {{
                            max-width: 600px;
                            margin: 0 auto;
                            background-color: #fff;
                            padding: 20px;
                            box-shadow: 0 0 10px rgba(0,0,0,0.1);
                          }}
                          .invoice-header {{
                            display: flex;
                            align-items: center;
                            margin-bottom: 20px;
                          }}
                          .invoice-header img {{
                            max-width: 100px;
                            height: auto;
                            margin-right: 20px;
                          }}
                          .invoice-header h2 {{
                            margin: 0;
                            font-size: 24px;
                          }}
                          .invoice-details {{
                            margin-bottom: 20px;
                          }}
                          .invoice-details h4 {{
                            margin-top: 0;
                            font-size: 18px;
                          }}
                          .invoice-table {{
                            width: 100%;
                            border-collapse: collapse;
                            margin-bottom: 20px;
                          }}
                          .invoice-table th, .invoice-table td {{
                            padding: 10px;
                            border: 1px solid #ccc;
                            text-align: left;
                          }}
                          .invoice-total {{
                            text-align: right;
                          }}
                        </style>
                        </head>
                        <body>
                          <div class=""invoice-container"">
                            <div class=""invoice-header"">
                              <h2>User Details</h2>
                            </div>
                            <div class=""invoice-details"">
                              <h4>Invoice Details</h4>
                              <p><strong>Invoice Number:</strong> INV-{invoiceNo}</p>
                              <p><strong>Invoice Date:</strong> {currentDate.ToString("yyyy MMMM dd dddd")}</p>
                            </div>
                            <div class=""user-details"">
                              <h4>User Details</h4>
                              <p><strong>Name:</strong> {request.UserName}</p>
                              <p><strong>Email:</strong> {request.Email}</p>
                              <p><strong>Address:</strong> Test</p>
                            </div>
                            <table class=""invoice-table"">
                              <thead>
                                <tr>
                                  <th>Description</th>
                                  <th>Quantity</th>
                                  <th>Unit Price</th>
                                  <th>Total</th>
                                </tr>
                              </thead>
                              <tbody>
                                <tr>
                                  <td> N/A </td>
                                  <td> N/A </td>
                                  <td> N/A </td>
                                  <td> N/A </td>
                                </tr>               
                              </tbody>
                              <tfoot>
                                <tr>
                                  <td colspan=""3"" class=""invoice-total"">Total:</td>
                                  <td> N/A </td>
                                </tr>
                              </tfoot>
                            </table>
                            <div class=""invoice-notes"">
                              <h4>Notes</h4>
                              <p>This is a sample.</p>
                            </div>
                          </div>
                        </body>
                        </html>
                    ";
			PdfGenerator.AddPdfPages(document, htmlContent, PageSize.A4);
			byte[] response;
			using (MemoryStream ms = new MemoryStream())
			{
				document.Save(ms);
				response = ms.ToArray();
			}
			string fileName = $"Test_Invoice_{invoiceNo}.pdf";
			return new FileContentResult(response, "application/pdf")
			{
				FileDownloadName = fileName
			};
		}
	}
}
