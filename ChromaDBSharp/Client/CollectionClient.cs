using ChromaDBSharp.Embeddings;
using ChromaDBSharp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChromaDBSharp.Client
{
    public class CollectionClient : ICollectionClient
    {
        private readonly HttpClient _httpClient;
        private readonly Collection _collection;

        public string CollectionId => _collection.Id;
        public string CollectionName => _collection.Name;
        public Collection Collection => _collection;
        private string CollectionApi => $"/api/v1/collections/{CollectionId}";
        private readonly IEmbeddable? _embeddingFunction;
        public CollectionClient(HttpClient httpClient, Collection collection, IEmbeddable? embeddingFunction = null) 
        {
            _httpClient = httpClient;
            _collection = collection;
            _embeddingFunction = embeddingFunction;
        }

        public QueryResult Query(IDictionary<string, object>? where = null,
            IDictionary<string, object>? whereDocument = null,
            IEnumerable<IEnumerable<float>>? queryEmbeddings = null,
            IEnumerable<string>? queryTexts = null,
            int numberOfResults = 10,
            IEnumerable<string>? include = null)
        {
            Task<QueryResult> queryTask = Task.Run(() => QueryAsync(where, whereDocument, queryEmbeddings, queryTexts, numberOfResults, include));
            return queryTask.Result;    
        }

        public async Task<QueryResult> QueryAsync(IDictionary<string, object>? where = null,
            IDictionary<string, object>? whereDocument = null,
            IEnumerable<IEnumerable<float>>? queryEmbeddings = null,
            IEnumerable<string>? queryTexts = null,
            int numberOfResults = 10,
            IEnumerable<string>? include = null)
        {
            if (queryEmbeddings == null && queryTexts == null)
            {
                throw new Exception("queryEmbeddings and queryTexts cannot both be undefined");
            }
            else if (queryEmbeddings == null && queryTexts != null)
            {
                if (_embeddingFunction != null)
                {
                    queryEmbeddings = await _embeddingFunction.Generate(queryTexts);
                }
                else
                {
                    throw new Exception("embeddingFunction is undefined. Please configure an embedding function");
                }
            }
            if (queryEmbeddings == null)
            {
                throw new Exception("embeddings is undefined but shouldnt be");
            }

            QueryRequest request = new QueryRequest(where, whereDocument, queryEmbeddings, numberOfResults, include);
            HttpResponseMessage response = await _httpClient.PostJsonAsync($"{CollectionApi}/query", request);
            string content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error querying collection {CollectionName}: {content}");
            }

            QueryResult queryResult = JsonConvert.DeserializeObject<QueryResult>(content)
                ?? throw new Exception($"Invalid query result: {content}");
            return queryResult;
        }

        public void Add(IEnumerable<string> ids, IEnumerable<IEnumerable<float>>? embeddings = null, IEnumerable<IDictionary<string, object>>? metadatas = null, IEnumerable<string>? documents = null)
        {
            Task addTask = Task.Run(() => AddAsync(ids, embeddings, metadatas, documents));
            addTask.Wait();
        }

        public async Task AddAsync(IEnumerable<string>? ids = null, IEnumerable<IEnumerable<float>>? embeddings = null, IEnumerable<IDictionary<string, object>>? metadatas = null, IEnumerable<string>? documents = null)
        {
            ValidationResult result = await Validate(true, ids, embeddings, metadatas, documents);
            CollectionRequest request = new CollectionRequest(result.Ids, result.Embeddings, result.Metadatas, result.Documents);
            HttpResponseMessage response = await _httpClient.PostJsonAsync($"{CollectionApi}/add", request);
            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error adding to collection {CollectionName}: {content}");
            }
        }

        public void Update(IEnumerable<string>? ids = null, IEnumerable<IEnumerable<float>>? embeddings = null, IEnumerable<IDictionary<string, object>>? metadatas = null, IEnumerable<string>? documents = null)
        {
            Task updateTask = Task.Run(() => UpdateAsync(ids,embeddings,metadatas,documents));
            updateTask.Wait();
        }

        public async Task UpdateAsync(IEnumerable<string>? ids = null, IEnumerable<IEnumerable<float>>? embeddings = null, IEnumerable<IDictionary<string, object>>? metadatas = null, IEnumerable<string>? documents = null)
        {
            ValidationResult result = await Validate(true, ids, embeddings, metadatas, documents);
            CollectionRequest request = new CollectionRequest(result.Ids, result.Embeddings, result.Metadatas, result.Documents);
            HttpResponseMessage response = await _httpClient.PostJsonAsync($"{CollectionApi}/update", request);
            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error updating collection {CollectionName}: {content}");
            }
        }

        public void Upsert(IEnumerable<string>? ids = null, IEnumerable<IEnumerable<float>>? embeddings = null, IEnumerable<IDictionary<string, object>>? metadatas = null, IEnumerable<string>? documents = null)
        {
            Task upsertTask = Task.Run(() => UpsertAsync(ids,embeddings,metadatas,documents));
            upsertTask.Wait();
        }

        public async Task UpsertAsync(IEnumerable<string>? ids = null, IEnumerable<IEnumerable<float>>? embeddings = null, IEnumerable<IDictionary<string, object>>? metadatas = null, IEnumerable<string>? documents = null)
        {
            ValidationResult result = await Validate(true, ids, embeddings, metadatas, documents);
            CollectionRequest request = new CollectionRequest(result.Ids, result.Embeddings, result.Metadatas, result.Documents);
            HttpResponseMessage response = await _httpClient.PostJsonAsync($"{CollectionApi}/upsert", request);
            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error upserting to collection {CollectionName}: {content}");
            }
        }

        public GetResult Get(IEnumerable<string>? ids = null, IDictionary<string, object>? where = null, int? limit = null, int? offset = null, IDictionary<string, object>? whereDocument = null, IEnumerable<string>? include = null)
        {
            Task<GetResult> getResultTask = Task.Run(() => GetAsync(ids, where, limit, offset, whereDocument, include));
            return getResultTask.Result;
        }

        public async Task<GetResult> GetAsync(IEnumerable<string>? ids = null, IDictionary<string, object>? where = null, int? limit = null, int? offset = null, IDictionary<string, object>? whereDocument = null, IEnumerable<string>? include = null)
        {
            GetRequest request = new GetRequest(ids, where, limit, offset, whereDocument, include);
            HttpResponseMessage response = await _httpClient.PostJsonAsync($"{CollectionApi}/get", request);
            string content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error getting from collection {CollectionName}: {content}");
            }

            GetResult getResult = JsonConvert.DeserializeObject<GetResult>(content)
                ?? throw new Exception($"Invalid collection get response: {content}");
            return getResult;
        }

        public void Delete(IEnumerable<string>? ids = null, IDictionary<string, object>? where = null, IDictionary<string, object>? whereDocument = null)
        {
            Task deleteTask = Task.Run(() => DeleteAsync(ids, where, whereDocument));
            deleteTask.Wait();
        }

        public async Task DeleteAsync(IEnumerable<string>? ids = null, IDictionary<string, object>? where = null, IDictionary<string, object>? whereDocument = null)
        {
            DeleteRequest request = new DeleteRequest(ids, where, whereDocument);
            HttpResponseMessage response = await _httpClient.PostJsonAsync($"{CollectionApi}/delete", request);
            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error calling delete for collection {CollectionName}: {content}");
            }
        }

        public int Count()
        {
            Task<int> countTask = Task.Run(() => CountAsync());
            return countTask.Result;
        }

        public async Task<int> CountAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{CollectionApi}/count");
            string content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error getting count for collection {CollectionName}: {content}");
            }

            int count = JsonConvert.DeserializeObject<int>(content);
            return count;
        }

        private async Task<ValidationResult> Validate(bool requireEmbeddingsOrDocuments, IEnumerable<string>? ids = null, IEnumerable<IEnumerable<float>>? embeddings = null, IEnumerable<IDictionary<string, object>>? metadatas = null, IEnumerable<string>? documents = null)
        {
            if (requireEmbeddingsOrDocuments)
            {
                if ((embeddings == null) && (documents == null))
                {
                    throw new Exception("Embeddings and documents cannot both be undefined");
                }
            }

            if ((embeddings == null) && (documents != null))
            {
                if (_embeddingFunction != null)
                {
                    embeddings = await _embeddingFunction.Generate(documents);
                }
                else
                {
                    throw new Exception("EmbeddingFunction is undefined. Please configure an embedding function");
                }
            }
            if (embeddings == null)
                throw new Exception("Embeddings is undefined but shouldn't be");
            if (
                (embeddings != null &&
                    ids.Count() != embeddings.Count()) ||
                (metadatas != null &&
                    ids.Count() != metadatas.Count()) ||
                (documents != null &&
                    ids.Count() != documents.Count())
            )
            {
                throw new Exception("ids, embeddings, metadatas, and documents must all be the same length");
            }

            bool hasDuplicateIds = ids.GroupBy(x => x).Where(v => v.Count() > 1).Any();

            if (hasDuplicateIds)
            {
                var duplicateIds = string.Join(",", ids.GroupBy(x => x).Where(v => v.Count() > 1).Select(v => v.Key));
                throw new Exception($"Expected IDs to be unique, found duplicates for: ${duplicateIds}");
            }

            return new ValidationResult(ids, embeddings, metadatas, documents);
        }
    }
}
