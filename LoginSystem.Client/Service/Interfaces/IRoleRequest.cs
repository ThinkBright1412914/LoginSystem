using LoginSystem.Utility;
using LoginSystem.ViewModel;
using Newtonsoft.Json;
using System.Text;

namespace LoginSystem.Client.Service.Interfaces
{
	public interface IRoleRequest
	{
		Task<List<RoleDTO>> GetRoles();
		Task<RoleDTO> CreateByAdminRole(RoleDTO model);
		Task<RoleDTO> DeleteByAdminRole(Guid Id);
	}

	public class RoleRequest : IRoleRequest
	{
		private readonly IhttpService _httpService;

		public RoleRequest(IhttpService httpService)
		{
			_httpService = httpService;
		}

		public async Task<List<RoleDTO>> GetRoles()
		{
			var (status, response) = await _httpService.GetAsync<List<RoleDTO>>(ApiUri.GetRoles);
			return response;
		}

		public async Task<RoleDTO> CreateByAdminRole(RoleDTO model)
		{
			string json = JsonConvert.SerializeObject(model);
			StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
			var (status, response) = await _httpService.PostAsync<RoleDTO>(ApiUri.CreateRole, content);
			return response;
		}

		public async Task<RoleDTO> DeleteByAdminRole(Guid Id)
		{
			var (status, response) = await _httpService.DeleteAsync<RoleDTO>(ApiUri.DeleteRole + "?Id=" + Id);
			return response;
		}
	}
}
