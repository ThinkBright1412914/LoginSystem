using LoginSystem.Utility;
using LoginSystem.ViewModel;
using Newtonsoft.Json;
using System.Text;

namespace LoginSystem.Client.Service.Interfaces
{
    public interface ICinemaRequest
    {
        Task<List<CinemaDto>> GetCinemas();
        Task<CinemaDto> GetCinemaById(int Id);
        Task<CinemaDto> Create(CinemaDto model);
        Task<CinemaDto> Update(CinemaDto model);
        Task<CinemaDto> Delete(int id);
    }

    public class CinemaRequest : ICinemaRequest
    {
        private readonly IhttpService _httpService;

        public CinemaRequest(IhttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<CinemaDto>> GetCinemas()
        {
            var (status, response) = await _httpService.GetAsync<List<CinemaDto>>(ApiUri.GetCinema);
            return response;
        }

        public async Task<CinemaDto> GetCinemaById(int Id)
        {
            var (status, response) = await _httpService.GetAsync<CinemaDto>(ApiUri.GetCinemaById + "?Id=" + Id);
            return response; ;
        }

        public async Task<CinemaDto> Create(CinemaDto model)
        {
            string json = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var (status, response) = await _httpService.PostAsync<CinemaDto>(ApiUri.CreateCinema, content);
            return response;
        }

        public async Task<CinemaDto> Delete(int id)
        {
            var (status, response) = await _httpService.DeleteAsync<CinemaDto>(ApiUri.DeleteCinema + "?Id=" + id);
            return response;
        }

        public async Task<CinemaDto> Update(CinemaDto model)
        {
            string json = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var (status, response) = await _httpService.PutAsync<CinemaDto>(ApiUri.UpdateCinema, content);
            return response;
        }
    }
}
