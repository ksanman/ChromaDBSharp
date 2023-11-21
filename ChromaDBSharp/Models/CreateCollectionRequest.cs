using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ChromaDBSharp.Models
{
    public class CreateCollectionRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("metadata")]
        public IDictionary<string, object>? Metadata { get; set; } = new Dictionary<string, object>();
        [JsonPropertyName("get_or_create")]
        public bool GetOrCreate { get; set; } = false;
    }
}
