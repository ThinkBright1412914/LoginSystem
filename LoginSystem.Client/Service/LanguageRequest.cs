using System.Text;
using LoginSystem.Client.Service.Interfaces;
using LoginSystem.Utility;
using LoginSystem.ViewModel;
using Newtonsoft.Json;

namespace LoginSystem.Client.Service
{
	public class LanguageRequest : ILanguageRequest
	{
		private readonly IhttpService _httpService;

		public LanguageRequest(IhttpService httpService)
		{
			_httpService = httpService;
		}

		public async Task<List<LanguagugeDto>> GetAll()
		{
			var (status, response) = await _httpService.GetAsync<List<LanguagugeDto>>(ApiUri.GetLanguages);
			return response;
		}

		public async Task<LanguagugeDto> Create(LanguagugeDto model)
		{
			string json = JsonConvert.SerializeObject(model);
			StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
			var (status, response) = await _httpService.PostAsync<LanguagugeDto>(ApiUri.CreateLanguage, content);
			return response;
		}

		public async Task<LanguagugeDto> Delete(int Id)
		{
			var (status, response) = await _httpService.DeleteAsync<LanguagugeDto>(ApiUri.DeleteLanguage + "?Id=" + Id);
			return response;
		}
	}
}
