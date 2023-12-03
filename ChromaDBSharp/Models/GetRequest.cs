using Newtonsoft.Json;
using System.Collections.Generic;

namespace ChromaDBSharp.Models
{
    internal class GetRequest
    {
        [JsonProperty("ids")]
        public IEnumerable<string>? Ids { get; set; } = null;
        [JsonProperty("where")]
        public IDictionary<string, object>? Where { get; set; } = null;
        [JsonProperty("limit")]
        public int? Limit { get; set; } = null;
        [JsonProperty("offset")]
        public int? Offset { get; set; } = null;
        [JsonProperty("where_document")]
        public IDictionary<string, object>? WhereDocument { get; set; } = null;
        [JsonProperty("include")]
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
