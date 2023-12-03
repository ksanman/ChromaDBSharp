using ChromaDBSharp.Models;
using Newtonsoft.Json;

namespace ChromaDBSharp.Tests
{
    internal static class ChromaDBClientTestHelper
    {

        public static HttpResponseMessage UpdateCollection(HttpRequestMessage message)
        {
            var json = message.Content.ReadAsStringAsync().Result;
            var content = JsonConvert.DeserializeObject<UpdateCollectionRequest>(json);
            var responseObject = new Collection
            {
                Id = "c4c1a65f-db2a-4372-a0b4-8e1f03ab6985",
                Database = "default_database",
                Tenant = "default_tenant",
                Name = content.NewName,
                Metadata = content.NewMetadata.Select(kv => new KeyValuePair<string, string>(kv.Key, kv.Value.ToString())).ToDictionary()
            };

            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(responseObject))
            };
            return response;
        }

        public static HttpResponseMessage CollectionName(HttpRequestMessage message)
        {
            switch (message.Method.Method)
            {
                case "DELETE": return DeleteCollection(message);
                case "GET": return GetCollection(message);
                case "PUT": return UpdateCollection(message);
                default: throw new NotImplementedException();
            }
        }

        public static HttpResponseMessage Collections(HttpRequestMessage message)
        {
            return message.Method.Method switch
            {
                "GET" => ListCollections(message),
                "POST" => CreateCollection(message),
                "DELETE" => DeleteCollection(message),
                _ => throw new NotImplementedException(),
            };
        }

        private static HttpResponseMessage CreateCollection(HttpRequestMessage message)
        {
            var requestTask = Task.Run(() => message.Content.ReadAsStringAsync());
            var requestJson = requestTask.Result;
            var request = JsonConvert.DeserializeObject<CreateCollectionRequest>(requestJson);
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
                Content = new StringContent(JsonConvert.SerializeObject(responseObject))
            };
            return response;
        }

        private static HttpResponseMessage DeleteCollection(HttpRequestMessage message)
        {
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        private static HttpResponseMessage GetCollection(HttpRequestMessage message)
        {
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
                Content = new StringContent(JsonConvert.SerializeObject(responseObject))
            };
            return response;
        }

        public static HttpResponseMessage Heartbeat(HttpRequestMessage message)
        {
            var responseObject = new HeartbeatResponse
            {
                Heartbeat = 1700805948712154400
            };

            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(responseObject))
            };
            return response;
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
                Content = new StringContent(JsonConvert.SerializeObject(responseObject))
            };
            return response;
        }

        public static HttpResponseMessage Reset(HttpRequestMessage message)
        {

            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(true))
            };
            return response;
        }

        public static HttpResponseMessage Version(HttpRequestMessage message)
        {
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent("0.4.17")
            };
            return response;
        }
    }
}