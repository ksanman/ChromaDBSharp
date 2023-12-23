using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace ChromaDBSharp.Models
{
    public class GetResult
    {
        [JsonProperty("ids")]
        public IEnumerable<string>? Ids { get; set; } = null;
        [JsonProperty("embeddings")]
        public IEnumerable<IEnumerable<float>>? Embeddings { get; set; } = null;
        [JsonProperty("metadatas")]
        public IEnumerable<IDictionary<string, string>>? Metadatas { get; set; } = null;
        [JsonProperty("documents")]
        public IEnumerable<string>? Documents { get; set; } = null;
        [JsonProperty("uris")]
        public IEnumerable<string>? URIs { get; set; } = null;
        [JsonProperty("data")]
        public IEnumerable<string>? Data { get; set; } = null;
    }
}
