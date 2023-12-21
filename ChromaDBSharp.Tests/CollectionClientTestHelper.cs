using ChromaDBSharp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromaDBSharp.Tests
{
    internal class CollectionClientTestHelper
    {
        public static HttpResponseMessage Add(HttpRequestMessage message)
        {
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        public static HttpResponseMessage Update(HttpRequestMessage message) 
        {
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        public static HttpResponseMessage Upsert(HttpRequestMessage message)
        {
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        public static HttpResponseMessage Delete(HttpRequestMessage message)
        {
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        public static HttpResponseMessage Get(HttpRequestMessage message)
        {
            GetResult result = new()
            {
                Ids = new[] { "Doc 1" },
                Documents = new[] { "This is a document" },
                Metadatas = new[] { new Dictionary<string, string> { { "source", "notion" } } },
                Embeddings = new[] { new[] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, } }
            };

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(result))
            };
        }

        public static HttpResponseMessage Count(HttpRequestMessage message)
        {
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(1))
            };
        }

        public static HttpResponseMessage Query(HttpRequestMessage message)
        {
            QueryResult result = new QueryResult();

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(result))
            };
        }
    }
}
