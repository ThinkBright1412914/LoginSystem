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

		public async Task<List<LanguageDto>> GetAll()
		{
			var (status, response) = await _httpService.GetAsync<List<LanguageDto>>(ApiUri.GetLanguages);
			return response;
		}

		public async Task<LanguageDto> Create(LanguageDto model)
		{
			string json = JsonConvert.SerializeObject(model);
			StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
			var (status, response) = await _httpService.PostAsync<LanguageDto>(ApiUri.CreateLanguage, content);
			return response;
		}

		public async Task<LanguageDto> Delete(int Id)
		{
			var (status, response) = await _httpService.DeleteAsync<LanguageDto>(ApiUri.DeleteLanguage + "?Id=" + Id);
			return response;
		}
	}
}
