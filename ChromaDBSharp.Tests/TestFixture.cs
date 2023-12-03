using ChromaDBSharp.Models;
using Newtonsoft.Json;

namespace ChromaDBSharp.Tests
{
    public class TestFixture : IDisposable
    {
        private readonly HttpClient _httpClient;
        public HttpClient Client => _httpClient;
        public TestFixture()
        {
            // Do "global" initialization here; Only called once.
            _httpClient = new HttpClient(new TestHttpMessageHandler(new Dictionary<string, Func<HttpRequestMessage, HttpResponseMessage>>
            {
                { "/api/v1/version", ChromaDBClientTestHelper.Version },
                { "/api/v1/heartbeat", ChromaDBClientTestHelper.Heartbeat },
                { "/api/v1/collections", ChromaDBClientTestHelper.Collections },
                { "/api/v1/reset", ChromaDBClientTestHelper.Reset },
                { "/api/v1/collections/test", ChromaDBClientTestHelper.CollectionName }
            }))
            {
                BaseAddress = new Uri("http://localhost:8000/")
            };

        }

        public void Dispose()
        {
            // Do "global" teardown here; Only called once.
            _httpClient.Dispose();
        }
    }
}
