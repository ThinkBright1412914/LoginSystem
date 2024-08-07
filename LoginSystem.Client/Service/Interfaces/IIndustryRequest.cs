using LoginSystem.Utility;
using LoginSystem.ViewModel;
using Newtonsoft.Json;
using System.Text;

namespace LoginSystem.Client.Service.Interfaces
{
	public interface IIndustryRequest
	{
		Task<List<IndustryDto>> GetAll();
		Task<IndustryDto> Create(IndustryDto model);
		Task<IndustryDto> Delete(int Id);
	}

	public class IndustryRequest : IIndustryRequest
	{
		private readonly IhttpService _httpService;

		public IndustryRequest(IhttpService httpService)
		{
			_httpService = httpService;
		}

		public async Task<List<IndustryDto>> GetAll()
		{
			var (status, response) = await _httpService.GetAsync<List<IndustryDto>>(ApiUri.GetIndustrys);
			return response;
		}

		public async Task<IndustryDto> Create(IndustryDto model)
		{
			string json = JsonConvert.SerializeObject(model);
			StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
			var (status, response) = await _httpService.PostAsync<IndustryDto>(ApiUri.CreateIndustry, content);
			return response;
		}

		public async Task<IndustryDto> Delete(int Id)
		{
			var (status, response) = await _httpService.DeleteAsync<IndustryDto>(ApiUri.DeleteIndustry + "?Id=" + Id);
			return response;
		}
	}
}
