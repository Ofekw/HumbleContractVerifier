using System.Net.Http;
using System.Threading.Tasks;

namespace HumbleVerifierLibrary
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> GetAsync(string requestUri);
        Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content);

        HttpClient Client { get; }
    }
}