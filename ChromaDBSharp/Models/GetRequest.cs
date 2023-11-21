using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ChromaDBSharp.Models
{
    internal class GetRequest
    {
        [JsonPropertyName("ids")]
        public IEnumerable<string>? Ids { get; set; } = null;
        [JsonPropertyName("where")]
        public IDictionary<string, object>? Where { get; set; } = null;
        [JsonPropertyName("limit")]
        public int? Limit { get; set; } = null;
        [JsonPropertyName("offset")]
        public int? Offset { get; set; } = null;
        [JsonPropertyName("where_document")]
        public IDictionary<string, object>? WhereDocument { get; set; } = null;
        [JsonPropertyName("include")]
        public IEnumerable<string>? Include { get; set; } = null;
        public GetRequest() { }
        public GetRequest(IEnumerable<string>? ids, IDictionary<string, object>? where, int? limit, int? offset, IDictionary<string, object>? whereDocument, IEnumerable<string>? include)
        {
            Ids = ids;
            Where = where;
            Limit = limit;
            Offset = offset;
            WhereDocument = whereDocument;
            Include = include;
        }
    }
}
