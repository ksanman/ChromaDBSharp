using ChromaDBSharp.Client;
using ChromaDBSharp.Models;

namespace ChromaDBSharp.Tests
{

    public class ChromaDBClientTest : IClassFixture<TestFixture>
    {
        private readonly HttpClient _httpClient;
        public ChromaDBClientTest(TestFixture fixture)
        {
            _httpClient = fixture.Client;
        }

        [Fact]
        public void Initialize()
        {
            ChromaDBClient client = new(_httpClient);
            Assert.NotNull(client);
        }

        [Fact]
        public void Version()
        {
            ChromaDBClient client = new(_httpClient);
            string version = client.Version();
            Assert.NotNull(version);
            Assert.NotEmpty(version);
        }

        [Fact]
        public void Heartbeat()
        {
            ChromaDBClient client = new(_httpClient);
            long heartbeat = client.Heartbeat();
            Assert.True(heartbeat > 0);
        }

        [Fact]
        public void ListCollections()
        {
            ChromaDBClient client = new(_httpClient);
            IEnumerable<Collection> collections = client.ListCollections();
            Assert.NotNull(collections);
        }

        [Fact]
        public void CreateCollection()
        {
            ChromaDBClient client = new(_httpClient);
            Assert.Throws<AggregateException>(() => client.CreateCollection("test"));
            ICollectionClient collection = client.CreateCollection("test", createOrGet: true);
            Assert.NotNull(collection);
        }

        [Fact]
        public void GetCollection()
        {
            ChromaDBClient client = new(_httpClient);
            ICollectionClient collection = client.GetCollection("test");
            Assert.NotNull(collection);
        }

        [Fact]
        public void UpdateCollection()
        {
            ChromaDBClient client = new(_httpClient);
            //TODO - need a better way for metadata, JSON does serialize correctly...
            client.UpdateCollection("test", "test", new Dictionary<string, object>
            {
                {"id1", "value1" }
            });
        }

        [Fact]
        public void DeleteCollection()
        {
            ChromaDBClient client = new(_httpClient);
            client.DeleteCollection("test");
        }
    }
}