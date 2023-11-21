using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ChromaDBSharp.Models
{
    internal class DeleteRequest
    {
        [JsonPropertyName("ids")]
        public IEnumerable<string>? Ids { get; set; } = null;
        [JsonPropertyName("where")]
        public IDictionary<string, object>? Where { get; set; } = null;
        [JsonPropertyName("where_document")]
        public IDictionary<string, object>? WhereDocument { get; set; } = null;
        public DeleteRequest() { }
        public DeleteRequest(IEnumerable<string>? ids, IDictionary<string, object>? where, IDictionary<string, object>? whereDocument)
        {
            Ids = ids;
            Where = where;
            WhereDocument = whereDocument;
        }
    }
}
