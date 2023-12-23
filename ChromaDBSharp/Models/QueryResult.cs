using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace ChromaDBSharp.Models
{
    public class QueryResult
    {
        [JsonProperty("ids")]
        public IEnumerable<IEnumerable<string>>? Ids { get; set; } = null;
        [JsonProperty("distances")]
        public IEnumerable<IEnumerable<float>>? Distances { get; set; } = null;
        [JsonProperty("metadatas")]
        public IEnumerable<IEnumerable<IDictionary<string, string>?>> Metadatas { get; set; } = Enumerable.Empty<IEnumerable<IDictionary<string, string>?>>();
        [JsonProperty("embeddings")]
        public IEnumerable<IEnumerable<float>>? Embeddings { get; set; } = null;
        [JsonProperty("documents")] 
        public IEnumerable<IEnumerable<string?>> Documents { get; set; } = Enumerable.Empty<IEnumerable<string?>>();
        [JsonProperty("uris")] 
        public IEnumerable<IEnumerable<string>>? Uris { get; set; } = null;
        [JsonProperty("data")] 
        public IEnumerable<IEnumerable<string>>? Data { get; set; } = null;
    }
}
