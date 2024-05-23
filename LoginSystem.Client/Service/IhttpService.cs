using System.Net;

namespace LoginSystem.Client.Service
{
    public interface IhttpService
    {
        Task<(HttpStatusCode statusCode, T responseType)> GetAsync<T>(string requestUri);
        Task<(HttpStatusCode statusCode, T responseType)> PostAsync<T>(string requestUri, HttpContent content);
        Task<(HttpStatusCode statusCode, T responseType)> PutAsync<T>(string requestUri, HttpContent content);
        Task<(HttpStatusCode statusCode, T responseType)> DeleteAsync<T>(string requestUri);
    }
}
