using LoginSystem.Client.Service.Interfaces;
using LoginSystem.Utility;
using LoginSystem.ViewModel;
using Newtonsoft.Json;
using System.Text;

namespace LoginSystem.Client.Service
{
    public class GenreRequest : IGenreRequest
    {
        private readonly IhttpService _httpService;

        public GenreRequest(IhttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<GenreDto>> GetAll()
        {
            var (status, response) = await _httpService.GetAsync<List<GenreDto>>(ApiUri.GetGenres);
            return response;
        }

        public async Task<GenreDto> Create(GenreDto model)
        {
            string json = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var (status, response) = await _httpService.PostAsync<GenreDto>(ApiUri.CreateGenre, content);
            return response;
        }

        public async Task<GenreDto> Delete(int Id)
        {
            var (status, response) = await _httpService.DeleteAsync<GenreDto>(ApiUri.DeleteGenre + "?Id=" + Id);
            return response;
        }

       
    }
}
