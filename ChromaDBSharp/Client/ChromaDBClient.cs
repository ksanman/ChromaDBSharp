using ChromaDBSharp.Embeddings;
using ChromaDBSharp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChromaDBSharp.Client
{
    public class ChromaDBClient : IChromaDBClient
    {
        private const string DEFAULT_TENANT = "default_tenant";
        private const string DEFAULT_DATABASE = "default_database";
        private readonly HttpClient _httpClient;
        private readonly string _tenant = DEFAULT_TENANT;
        private readonly string _database = DEFAULT_DATABASE;
        public ChromaDBClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public ChromaDBClient(HttpClient httpClient, string tenant, string database) : this(httpClient)
        {
            _tenant = tenant;
            _database = database;
        }

        public ICollectionClient CreateCollection(string name, IDictionary<string, object>? metadata = null, IEmbeddable? embeddingFunction = null, bool createOrGet = false)
        {
            Task<ICollectionClient> collectionTask = Task.Run(() => CreateCollectionAsync(name, metadata, embeddingFunction, createOrGet));
            return collectionTask.Result;
        }

        public async Task<ICollectionClient> CreateCollectionAsync(string name, IDictionary<string, object>? metadata = null, IEmbeddable? embeddingFunction = null, bool getOrCreate = false)
        {
            CreateCollectionRequest request = new CreateCollectionRequest
            {
                Name = name,
                Metadata = metadata,
                GetOrCreate = getOrCreate
            };

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/v1/collections", request);
            string content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error creating collection: {content}");
            }

            Collection collection = JsonSerializer.Deserialize<Collection>(content) 
                ?? throw new Exception($"Create Collection returned invalid response {content}");
            return new CollectionClient(_httpClient, collection, embeddingFunction);
        }

        public void DeleteCollection(string name)
        {
            Task deleteTask = Task.Run( () => DeleteCollectionAsync(name));
            deleteTask.Wait(); 
        }

        public async Task DeleteCollectionAsync(string name)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/v1/collections/{name}?tenant={_tenant}&database={_database}");

            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error deleting collection {name}: {content}");
            }
        }

        public ICollectionClient GetCollection(string name, IEmbeddable? embeddingFunction = null)
        {
            Task<ICollectionClient> getTask = Task.Run(() => GetCollectionAsync(name, embeddingFunction));
            return getTask.Result;
        }

        public async Task<ICollectionClient> GetCollectionAsync(string name, IEmbeddable? embeddingFunction = null)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/v1/collections/{name}?tenant={_tenant}&database={_database}");
            string content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error getting collection {name}: {content}");
            }

            Collection collection = JsonSerializer.Deserialize<Collection>(content)
                ?? throw new Exception($"Invalid collection response: {content}");
            return new CollectionClient(_httpClient, collection, embeddingFunction);
        }

        public long Heartbeat()
        {
            Task<long> heartbeatTask = Task.Run(() => HeartbeatAsync());
            return heartbeatTask.Result;
        }

        public async Task<long> HeartbeatAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/api/v1/heartbeat");
            string content = await response.Content.ReadAsStringAsync();

            if(!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error getting ChromaDB heartbeat: {content}");
            }

            HeartbeatResponse? heartbeatResponse = JsonSerializer.Deserialize<HeartbeatResponse>(content);
            return heartbeatResponse == null
                ? throw new Exception($"Invalid heartbeat response from ChromaDB {content}")
                : heartbeatResponse.Heartbeat;
        }

        public IEnumerable<Collection> ListCollections()
        {
            Task<IEnumerable<Collection>> collectionTask = Task.Run(() => ListCollectionsAsync());
            return collectionTask.Result;
        }

        public async Task<IEnumerable<Collection>> ListCollectionsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/v1/collections?tenant={_tenant}&database={_database}");
            string content = await response.Content.ReadAsStringAsync();
            if(!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error getting Collections from ChromaDB: {content}");
            }

            IEnumerable<Collection>? collections = JsonSerializer.Deserialize<IEnumerable<Collection>>(content);
            return collections ?? throw new Exception($"Invalid response from ListCollections: {content}");
        }

        public bool Reset()
        {
            Task<bool> resetTask = Task.Run(() => ResetAsync());
            return resetTask.Result;
        }

        public async Task<bool> ResetAsync()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "api/v1/reset");
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error resetting ChromaDB: {content}");
            }
            bool responseValue = JsonSerializer.Deserialize<bool>(content);
            return responseValue;
        }

        public void UpdateCollection(string collectionId, string? name = null, IDictionary<string, object>? metadata = null)
        {
            Task updateTask = Task.Run(() => UpdateCollectionAsync(collectionId, name, metadata));
            updateTask.Wait();
        }

        public async Task UpdateCollectionAsync(string collectionId, string? name = null, IDictionary<string, object>? metadata = null)
        {
            if (string.IsNullOrWhiteSpace(name) && metadata == null) throw new ArgumentException("Name or Metadata must include data.");
            UpdateCollectionRequest request = new UpdateCollectionRequest
            {
                NewName = name,
                NewMetadata = metadata
            };
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/v1/collections/{collectionId}", request);

            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to update collection {collectionId}: {content}");
            }
        }

        public string Version()
        {
            Task<string> versionTask = Task.Run(() => VersionAsync());
            return versionTask.Result;
        }

        public async Task<string> VersionAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/api/v1/version");
            string content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error getting ChromaDB version. {content}");
            }

            return content;
        }
    }
}
