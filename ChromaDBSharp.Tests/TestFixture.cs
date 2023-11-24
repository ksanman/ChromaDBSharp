using ChromaDBSharp.Models;
using System.Net.Http.Json;

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
                { "/api/v1/version", Version },
                { "/api/v1/heartbeat", Heartbeat },
                { "/api/v1/collections", Collections }
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

        private static HttpResponseMessage Version(HttpRequestMessage message)
        {
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent("0.4.17")
            };
            return response;
        }

        private static HttpResponseMessage Heartbeat(HttpRequestMessage message)
        {
            var responseObject = new HeartbeatResponse
            {
                Heartbeat = 1700805948712154400
            };

            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = JsonContent.Create(responseObject)
            };
            return response;
        }

        private static HttpResponseMessage Collections(HttpRequestMessage message)
        {
            return message.Method.Method switch
            {
                "GET" => ListCollections(message),
                "POST" => CreateCollection(message),
                _ => throw new NotImplementedException(),
            };
        }

        private static HttpResponseMessage ListCollections(HttpRequestMessage message)
        {
            var responseObject = new List<Collection>
            {
                new() {
                    Id = "c4c1a65f-db2a-4372-a0b4-8e1f03ab6985",
                    Database = "default_database",
                    Tenant = "default_tenant",
                    Name = "Test",
                    Metadata = new Dictionary<string, string>
                    {
                        { "TestMD", "value" }
                    }
                }
            };

            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = JsonContent.Create(responseObject)
            };
            return response;
        }

        private static HttpResponseMessage CreateCollection(HttpRequestMessage message)
        {
            var requestTask = Task.Run(() => message.Content.ReadFromJsonAsync<CreateCollectionRequest>());
            var request = requestTask.Result;
            if (!request.GetOrCreate)
            {
                var error = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("{\r\n  \"error\": \"UniqueConstraintError('Collection test already exists')\"\r\n}")
                };
                return error;
            }

            var responseObject = new Collection 
            {
                Id = "c4c1a65f-db2a-4372-a0b4-8e1f03ab6985",
                Database = "default_database",
                Tenant = "default_tenant",
                Name = "Test",
                Metadata = new Dictionary<string, string>
                {
                    { "TestMD", "value" }
                }
            };

            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = JsonContent.Create(responseObject)
            };
            return response;
        }
    }
}
