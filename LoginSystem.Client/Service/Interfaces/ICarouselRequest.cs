using LoginSystem.Utility;
using LoginSystem.ViewModel;
using Newtonsoft.Json;
using System.Text;

namespace LoginSystem.Client.Service.Interfaces
{
    public interface ICarouselRequest
    {
        Task<List<CarouselDto>> GetAll();
        Task<CarouselDto> Create(CarouselDto model);
        Task<CarouselDto> Delete(int Id);
    }

	public class CarouselRequest : ICarouselRequest
	{
		private readonly IhttpService _httpService;

		public CarouselRequest(IhttpService httpService)
		{
			_httpService = httpService;
		}

		public async Task<List<CarouselDto>> GetAll()
		{
			var (status, response) = await _httpService.GetAsync<List<CarouselDto>>(ApiUri.GetCarousels);
			return response;
		}

		public async Task<CarouselDto> Create(CarouselDto model)
		{
			string json = JsonConvert.SerializeObject(model);
			StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
			var (status, response) = await _httpService.PostAsync<CarouselDto>(ApiUri.CreateCarousel, content);
			return response;
		}

		public async Task<CarouselDto> Delete(int Id)
		{
			var (status, response) = await _httpService.DeleteAsync<CarouselDto>(ApiUri.DeleteCarousel + "?Id=" + Id);
			return response;
		}


	}
}
