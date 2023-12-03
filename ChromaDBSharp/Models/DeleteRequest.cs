using Newtonsoft.Json;
using System.Collections.Generic;

namespace ChromaDBSharp.Models
{
    internal class DeleteRequest
    {
        [JsonProperty("ids")]
        public IEnumerable<string>? Ids { get; set; } = null;
        [JsonProperty("where")]
        public IDictionary<string, object>? Where { get; set; } = null;
        [JsonProperty("where_document")]
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
