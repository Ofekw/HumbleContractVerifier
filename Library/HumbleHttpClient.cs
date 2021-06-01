namespace HumbleVerifierLibrary
{
    using System.Net.Http;
    using System.Threading.Tasks;

    public class HumbleHttpClient : IHttpClient
    {
        public HumbleHttpClient(HttpClient httpClient)
        {
            this.Client = httpClient;
        }

        public HttpClient Client { get; }

        public Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return this.Client.GetAsync(requestUri);
        }

        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {
            return this.Client.PostAsync(requestUri, content);
        }
    }
}