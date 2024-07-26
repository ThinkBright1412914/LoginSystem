using LoginSystem.Client.Service.Interfaces;
using LoginSystem.Utility;
using LoginSystem.ViewModel;
using Newtonsoft.Json;
using System.Text;

namespace LoginSystem.Client.Service
{
    public class MovieRequest : IMovieRequest
    {
        private readonly IhttpService _httpService;

        public MovieRequest(IhttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<MovieDto>> GetMovies()
        {
            var (status, response) = await _httpService.GetAsync<List<MovieDto>>(ApiUri.GetMovies);
            return response;
        }

        public async Task<MovieDto> GetMovieById(int Id)
        {
            var (status, response) = await _httpService.GetAsync<MovieDto>(ApiUri.GetMovieById + "?Id=" + Id);
            return response; ;
        }

        public async Task<MovieDto> Create(MovieDto model)
        {
            string json = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var (status, response) = await _httpService.PostAsync<MovieDto>(ApiUri.CreateMovie, content);
            return response;
        }

        public async Task<MovieDto> Update(MovieDto model)
        {
            string json = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var (status, response) = await _httpService.PutAsync<MovieDto>(ApiUri.UpdateMovie, content);
            return response;
        }

        public async Task<MovieDto> Delete(int Id)
        {
            var (status, response) = await _httpService.DeleteAsync<MovieDto>(ApiUri.DeleteMovie + "?Id=" + Id);
            return response;
        }

    }
}
