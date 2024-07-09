using LoginSystem.Client.Models;
using LoginSystem.Client.Service.Interfaces;
using LoginSystem.Utility;
using Newtonsoft.Json;
using System.Text;

namespace LoginSystem.Client.Service
{
    public class RoleRequest : IRoleRequest
	{
		private readonly IhttpService _httpService;

		public RoleRequest(IhttpService httpService)
		{
			_httpService = httpService;
		}

		public async Task<List<RoleVM>> GetRoles()
		{
			var (status, response) = await _httpService.GetAsync<List<RoleVM>>(ApiUri.GetRoles);
			return response;
		}

		public async Task<RoleVM> CreateByAdminRole(RoleVM model)
		{
			string json = JsonConvert.SerializeObject(model);
			StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
			var (status, response) = await _httpService.PostAsync<RoleVM>(ApiUri.CreateRole, content);
			return response;
		}

		public async Task<RoleVM> DeleteByAdminRole(RoleVM model)
		{
			var (status, response) = await _httpService.DeleteAsync<RoleVM>(ApiUri.DeleteRole + "?Id=" + model.RoleId);
			return response;
		}
	}
}
