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
                { "/api/v1/collections/test", ChromaDBClientTestHelper.CollectionName },
                { "/api/v1/collections/c12e442f-4651-4bcd-83c0-ed4f696977f6/add", CollectionClientTestHelper.Add },
                { "/api/v1/collections/c12e442f-4651-4bcd-83c0-ed4f696977f6/update", CollectionClientTestHelper.Update },
                { "/api/v1/collections/c12e442f-4651-4bcd-83c0-ed4f696977f6/upsert", CollectionClientTestHelper.Upsert },
                { "/api/v1/collections/c12e442f-4651-4bcd-83c0-ed4f696977f6/get", CollectionClientTestHelper.Get },
                { "/api/v1/collections/c12e442f-4651-4bcd-83c0-ed4f696977f6/delete", CollectionClientTestHelper.Delete },
                { "/api/v1/collections/c12e442f-4651-4bcd-83c0-ed4f696977f6/count", CollectionClientTestHelper.Count },
                { "/api/v1/collections/c12e442f-4651-4bcd-83c0-ed4f696977f6/query", CollectionClientTestHelper.Query }
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
