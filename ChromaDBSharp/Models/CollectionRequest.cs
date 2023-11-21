using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ChromaDBSharp.Models
{
    internal class CollectionRequest
    {
        [JsonPropertyName("ids")]
        public IEnumerable<string>? Ids { get; set; } = null;
        [JsonPropertyName("embeddings")]
        public IEnumerable<IEnumerable<float>>? Embeddings { get; set; } = null;
        public IDictionary<string, object>? Metadatas { get; set; } = null;
        public IEnumerable<string>? Documents { get; set; } = null;
        public CollectionRequest() { }
        public CollectionRequest(IEnumerable<string>? ids, IEnumerable<IEnumerable<float>>? embeddings, IDictionary<string, object>? metadatas, IEnumerable<string>? documents)
        {
            Ids = ids;
            Embeddings = embeddings;
            Metadatas = metadatas;
            Documents = documents;
        }
    }
}
