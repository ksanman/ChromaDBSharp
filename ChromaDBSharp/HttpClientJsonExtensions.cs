using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChromaDBSharp
{
    internal static class HttpClientJsonExtensions
    {
        public static Task<HttpResponseMessage> PostJsonAsync<T>(this HttpClient httpClient, string url, T request)
        {
            HttpContent requestContent = new StringContent(JsonConvert.SerializeObject(request));
            return httpClient.PostAsync(url, requestContent);
        }

        public static Task<HttpResponseMessage> PutJsonAsync<T>(this HttpClient httpClient, string url, T request)
        {
            HttpContent requestContent = new StringContent(JsonConvert.SerializeObject(request));
            return httpClient.PutAsync(url, requestContent);
        }
    }
}
