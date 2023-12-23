using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChromaDBSharp
{
    internal static class HttpClientJsonExtensions
    {
        public static Task<HttpResponseMessage> PostJsonAsync<T>(this HttpClient httpClient, string url, T request)
        {
            HttpContent requestContent = new StringContent(JsonConvert.SerializeObject(request), encoding: Encoding.UTF8, "application/json");
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = requestContent
            };
            return httpClient.SendAsync(requestMessage);
        }

        public static Task<HttpResponseMessage> PutJsonAsync<T>(this HttpClient httpClient, string url, T request)
        {
            HttpContent requestContent = new StringContent(JsonConvert.SerializeObject(request), encoding: Encoding.UTF8, "application/json");
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = requestContent
            };
            return httpClient.SendAsync(requestMessage);
        }
    }
}
