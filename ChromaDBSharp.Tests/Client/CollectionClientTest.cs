using ChromaDBSharp.Models;
using ChromaDBSharp.Tests;

namespace ChromaDBSharp.Client.Tests
{
    public class CollectionClientTest : IClassFixture<TestFixture>
    {
        private readonly HttpClient _httpClient;
        public CollectionClientTest(TestFixture fixture)
        {
            _httpClient = fixture.Client;
        }

        [Fact()]
        public void InitializeTest()
        {
            Collection collection = new()
            {
                Id = "c12e442f-4651-4bcd-83c0-ed4f696977f6",
                Database = "default",
                Tenant = "default",
                Metadata = new Dictionary<string, string>(),
                Name = "test"
            };

            CollectionClient client = new(_httpClient, collection);
            Assert.NotNull(client);
        }

        [Fact()]
        public void QueryTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void QueryAsyncTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void AddTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void AddAsyncTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void UpdateTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void UpdateAsyncTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void UpsertTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void UpsertAsyncTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void GetTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void GetAsyncTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void DeleteTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void DeleteAsyncTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void CountTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void CountAsyncTest()
        {
            Assert.True(false, "This test needs an implementation");
        }
    }
}