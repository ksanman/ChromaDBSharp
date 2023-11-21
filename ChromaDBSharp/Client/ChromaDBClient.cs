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

        public Collection CreateCollection(string name, IDictionary<string, object>? metadata = null, bool createOrGet = false)
        {
            Task<Collection> collectionTask = Task.Run(() => CreateCollectionAsync(name, metadata, createOrGet));
            return collectionTask.Result;
        }

        public async Task<Collection> CreateCollectionAsync(string name, IDictionary<string, object>? metadata = null, bool getOrCreate = false)
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

            Collection? collection = JsonSerializer.Deserialize<Collection>(content);
            return collection ?? throw new Exception($"Create Collection returned invalid response {content}");
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
