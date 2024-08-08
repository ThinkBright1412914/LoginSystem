using LoginSystem.Utility;
using LoginSystem.ViewModel;
using Newtonsoft.Json;
using System.Text;

namespace LoginSystem.Client.Service.Interfaces
{
    public interface IShowRequest
    {
        Task<List<ShowDto>> GetShow();
        Task<ShowDto> GetShowById(int id);
        Task<ShowDto> Create(ShowDto model);
        Task<ShowDto> Update(ShowDto model);
        Task<ShowDto> Delete(int Id);
    }

    public class ShowRequest : IShowRequest
    {
        private readonly IhttpService _httpService;
        public ShowRequest(IhttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<ShowDto>> GetShow()
        {
            var (status, response) = await _httpService.GetAsync<List<ShowDto>>(ApiUri.GetShows);
            return response;
        }

        public async Task<ShowDto> GetShowById(int id)
        {
            var (status, response) = await _httpService.GetAsync<ShowDto>(ApiUri.GetShowById + "?Id=" + id);
            return response;
        }

        public async Task<ShowDto> Create(ShowDto model)
        {
            string json = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var (status, response) = await _httpService.PostAsync<ShowDto>(ApiUri.CreateShow, content);
            return response;
        }

        public async Task<ShowDto> Update(ShowDto model)
        {
            string json = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var (status, response) = await _httpService.PutAsync<ShowDto>(ApiUri.UpdateShow, content);
            return response;
        }

        public async Task<ShowDto> Delete(int Id)
        {
            var (status, response) = await _httpService.DeleteAsync<ShowDto>(ApiUri.DeleteShow + "?Id=" + Id);
            return response;
        }
    }
}
