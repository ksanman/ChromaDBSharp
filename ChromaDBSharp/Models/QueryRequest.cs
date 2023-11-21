using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ChromaDBSharp.Models
{
    internal class QueryRequest
    {
        [JsonPropertyName("where")]
        public IDictionary<string, object>? Where { get; set; } = null;
        [JsonPropertyName("where_document")]
        public IDictionary<string, object>? WhereDocument { get; set; } = null;
        [JsonPropertyName("query_embeddings")]
        public IEnumerable<IEnumerable<float>>? QueryEmbeddings { get; set; } = null;
        [JsonPropertyName("n_results")]
        public int NumberOfResults { get; set; } = 10;
        [JsonPropertyName("include")]
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
