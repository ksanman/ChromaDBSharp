using Newtonsoft.Json;
using System.Collections.Generic;

namespace ChromaDBSharp.Models
{
    public class CreateCollectionRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
        [JsonProperty("metadata")]
        public IDictionary<string, object>? Metadata { get; set; } = new Dictionary<string, object>();
        [JsonProperty("get_or_create")]
        public bool GetOrCreate { get; set; } = false;
    }
}
