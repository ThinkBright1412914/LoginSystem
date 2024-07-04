using Newtonsoft.Json;
using System.Net;
using System.Net.Http;

namespace LoginSystem.Client.Service
{
    public class HttpService : IhttpService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly SessionService _session;
        private readonly IHttpContextAccessor _contextAccessor;

        public HttpService(IHttpClientFactory httpClient, SessionService session, IHttpContextAccessor contextAccessor)
        {
            _httpClient = httpClient;
            _session = session;
            _contextAccessor = contextAccessor;
        }

        private string baseUrl()
        {
            var request = _contextAccessor.HttpContext?.Request;
            var getUrl = request?.Path.Value;
            return getUrl;
        }      

        public async Task<(HttpStatusCode statusCode, T responseType)> GetAsync<T>(string requestUri)
        {
            var client = _httpClient.CreateClient("LoginApi");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _session.GetToken());
            var response = await client.GetAsync(requestUri);
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<T>(responseString);
            return (response.StatusCode, responseObj);
        }

        public async Task<(HttpStatusCode statusCode, T responseType)> PostAsync<T>(string requestUri, HttpContent content)
        {
            var client = _httpClient.CreateClient("LoginApi");
            if (baseUrl() == "/AdminUser/Create")
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _session.GetToken());
            }
            var response = await client.PostAsync(requestUri, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<T>(responseString);

            return (response.StatusCode, responseObj);
        }

        public async Task<(HttpStatusCode statusCode, T responseType)> PutAsync<T>(string requestUri, HttpContent content)
        {
            var client = _httpClient.CreateClient("LoginApi");
            if (baseUrl() == "/AdminUser/UpdateUser")
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _session.GetToken());
            }
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _session.GetToken());
            var response = await client.PutAsync(requestUri, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<T>(responseString);

            return (response.StatusCode, responseObj);
        }
        public async Task<(HttpStatusCode statusCode, T responseType)> DeleteAsync<T>(string requestUri)
        {
            var client = _httpClient.CreateClient("LoginApi");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _session.GetToken());
            var response = await client.DeleteAsync(requestUri);
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<T>(responseString);
            return (response.StatusCode, responseObj);
        }
    }
}
