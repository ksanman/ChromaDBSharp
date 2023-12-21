using ChromaDBSharp.Client;
using ChromaDBSharp.Embeddings;
using ChromaDBSharp.Models;

namespace ChromaDBSharp.Tests
{
    public class CollectionClientTest : IClassFixture<TestFixture>
    {
        private readonly HttpClient _httpClient;
        private readonly IEmbeddable _embedder;
        public CollectionClientTest(TestFixture fixture)
        {
            _httpClient = fixture.Client;
            _embedder = new TestEmbedder();
        }

        [Fact()]
        public void InitializeTest()
        {
            CollectionClient client = CreateClient();
            Assert.NotNull(client);
        }

        [Fact()]
        public void QueryTest()
        {
            CollectionClient client = CreateClient();
            QueryResult result = client.Query(queryTexts: new[] { "This is a query document" }, numberOfResults: 2);
            Assert.NotNull(result);
        }

        [Fact()]
        public async Task QueryAsyncTest()
        {
            CollectionClient client = CreateClient();
            QueryResult result = await client.QueryAsync(queryTexts: new[] { "This is a query document" }, numberOfResults: 2);
            Assert.NotNull(result);
        }

        [Fact()]
        public void AddTest()
        {
            CollectionClient client = CreateClient();
            client.Add(documents: new[] { "This is document 1", "This is document 2" },
                metadatas: new[] { new Dictionary<string, object> { { "source", "notion" } }, new Dictionary<string, object> { { "source", "google-docs" } } },
                ids: new[] { "doc 1", "doc 2" });
        }

        [Fact()]
        public async Task AddAsyncTest()
        {
            CollectionClient client = CreateClient();
            await client.AddAsync(documents: new[] { "This is document 1", "This is document 2" },
                metadatas: new[] { new Dictionary<string, object> { { "source", "notion" } }, new Dictionary<string, object> { { "source", "google-docs" } } },
                ids: new[] { "doc 1", "doc 2" });
        }

        [Fact()]
        public void UpdateTest()
        {
            CollectionClient client = CreateClient();
            client.Update(documents: new[] { "This is document 1", "This is document 2" },
                metadatas: new[] { new Dictionary<string, object> { { "source", "notion" } }, new Dictionary<string, object> { { "source", "google-docs" } } },
                ids: new[] { "doc 1", "doc 2" });
        }

        [Fact()]
        public async Task UpdateAsyncTest()
        {
            CollectionClient client = CreateClient();
            await client.UpdateAsync(documents: new[] { "This is document 1", "This is document 2" },
                metadatas: new[] { new Dictionary<string, object> { { "source", "notion" } }, new Dictionary<string, object> { { "source", "google-docs" } } },
                ids: new[] { "doc 1", "doc 2" });
        }

        [Fact()]
        public void UpsertTest()
        {
            CollectionClient client = CreateClient();
            client.Upsert(documents: new[] { "This is document 1", "This is document 2" },
                metadatas: new[] { new Dictionary<string, object> { { "source", "notion" } }, new Dictionary<string, object> { { "source", "google-docs" } } },
                ids: new[] { "doc 1", "doc 2" });
        }

        [Fact()]
        public async Task UpsertAsyncTest()
        {
            CollectionClient client = CreateClient();
            await client.UpsertAsync(documents: new[] { "This is document 1", "This is document 2" },
                metadatas: new[] { new Dictionary<string, object> { { "source", "notion" } }, new Dictionary<string, object> { { "source", "google-docs" } } },
                ids: new[] { "doc 1", "doc 2" });
        }

        [Fact()]
        public void GetTest()
        {
            CollectionClient client = CreateClient();
            GetResult result = client.Get(ids: new[] { "doc 1"});
        }

        [Fact()]
        public async Task GetAsyncTest()
        {
            CollectionClient client = CreateClient();
            GetResult result = await client.GetAsync(ids: new[] { "doc 1" });
        }

        [Fact()]
        public void DeleteTest()
        {
            CollectionClient client = CreateClient();
            client.Delete(ids: new[] { "doc 1" });
        }

        [Fact()]
        public async Task DeleteAsyncTest()
        {
            CollectionClient client = CreateClient();
            await client.DeleteAsync(ids: new[] { "doc 1" });
        }

        [Fact()]
        public void CountTest()
        {
            CollectionClient client = CreateClient();
            int collectionCount = client.Count();
            Assert.Equal(1, collectionCount);
        }

        [Fact()]
        public async Task CountAsyncTest()
        {
            CollectionClient client = CreateClient();
            int collectionCount = await client.CountAsync();
            Assert.Equal(1, collectionCount);
        }

        private CollectionClient CreateClient()
        {
            Collection collection = new()
            {
                Id = "c12e442f-4651-4bcd-83c0-ed4f696977f6",
                Database = "default",
                Tenant = "default",
                Metadata = new Dictionary<string, string>(),
                Name = "test"
            };
            
            CollectionClient client = new(_httpClient, collection, _embedder);
            return client;
        }
    }

    internal sealed class TestEmbedder : IEmbeddable
    {
        public Task<IEnumerable<IEnumerable<float>>> Generate(IEnumerable<string> texts)
        {
            var embeddings = texts.Select(t => t.Select(c => Convert.ToSingle(char.GetNumericValue(c))));
            return Task.FromResult(embeddings);
        }
    }
}