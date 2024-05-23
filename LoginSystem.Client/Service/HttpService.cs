using Newtonsoft.Json;
using System.Net;
using System.Net.Http;

namespace LoginSystem.Client.Service
{
    public class HttpService : IhttpService
    {
        private readonly IHttpClientFactory _httpClient;

        public HttpService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(HttpStatusCode statusCode, T responseType)> DeleteAsync<T>(string requestUri)
        {
            var client = _httpClient.CreateClient("LoginApi");
            var response = await client.GetAsync(requestUri);
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<T>(responseString);
            return (response.StatusCode, responseObj);
        }

        public async Task<(HttpStatusCode statusCode, T responseType)> GetAsync<T>(string requestUri)
        {
            var client = _httpClient.CreateClient("LoginApi");
            var response = await client.GetAsync(requestUri);
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<T>(responseString); 
            return (response.StatusCode, responseObj);
        }

        public async Task<(HttpStatusCode statusCode, T responseType)> PostAsync<T>(string requestUri, HttpContent content)
        {
            var client = _httpClient.CreateClient("LoginApi");
            var response = await client.PostAsync(requestUri, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<T>(responseString);

            return (response.StatusCode, responseObj);
        }

        public async Task<(HttpStatusCode statusCode, T responseType)> PutAsync<T>(string requestUri, HttpContent content)
        {
            var client = _httpClient.CreateClient("LoginApi");
            var response = await client.PutAsync(requestUri, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<T>(responseString);

            return (response.StatusCode, responseObj);
        }
    }
}
