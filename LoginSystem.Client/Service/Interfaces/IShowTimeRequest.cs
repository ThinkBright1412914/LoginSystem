using LoginSystem.Utility;
using LoginSystem.ViewModel;
using Newtonsoft.Json;
using System.Text;

namespace LoginSystem.Client.Service.Interfaces
{
	public interface IShowTimeRequest
	{
		Task<List<ShowTimeDto>> GetAll();
		Task<ShowTimeDto> Create(ShowTimeDto model);
		Task<ShowTimeDto> Delete(int Id);
	}

	public class ShowTimeRequest : IShowTimeRequest
	{
		private readonly IhttpService _httpService;

		public ShowTimeRequest(IhttpService httpService)
		{
			_httpService = httpService;
		}

		public async Task<List<ShowTimeDto>> GetAll()
		{
			var (status, response) = await _httpService.GetAsync<List<ShowTimeDto>>(ApiUri.GetShowTime);
			return response;
		}

		public async Task<ShowTimeDto> Create(ShowTimeDto model)
		{
			string json = JsonConvert.SerializeObject(model);
			StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
			var (status, response) = await _httpService.PostAsync<ShowTimeDto>(ApiUri.CreateShowTime, content);
			return response;
		}

		public async Task<ShowTimeDto> Delete(int Id)
		{
			var (status, response) = await _httpService.DeleteAsync<ShowTimeDto>(ApiUri.DeleteShowTime + "?Id=" + Id);
			return response;
		}	
	}
}
