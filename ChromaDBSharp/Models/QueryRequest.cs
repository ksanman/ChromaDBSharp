using System.Collections.Generic;
using Newtonsoft.Json;

namespace ChromaDBSharp.Models
{
    internal class QueryRequest
    {
        [JsonProperty("where")]
        public IDictionary<string, object>? Where { get; set; } = null;
        [JsonProperty("where_document")]
        public IDictionary<string, object>? WhereDocument { get; set; } = null;
        [JsonProperty("query_embeddings")]
        public IEnumerable<IEnumerable<float>>? QueryEmbeddings { get; set; } = null;
        [JsonProperty("n_results")]
        public int NumberOfResults { get; set; } = 10;
        [JsonProperty("include")]
        public IEnumerable<string> Include = new List<string> { "metadatas", "documents", "distances" };
        public QueryRequest() { }
        public QueryRequest(IDictionary<string, object>? where, IDictionary<string, object>? whereDocument, IEnumerable<IEnumerable<float>>? queryEmbeddings, int? numberOfResults, IEnumerable<string>? include)
        {
            Where = where;
            WhereDocument = whereDocument;
            QueryEmbeddings = queryEmbeddings;
            NumberOfResults = numberOfResults ?? 10;
            Include = include ?? new List<string> { "metadatas", "documents", "distances" };
        }
    }
}
