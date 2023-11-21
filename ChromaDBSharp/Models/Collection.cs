using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ChromaDBSharp.Models
{
    public class Collection
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        [JsonPropertyName("metadata")]
        public IDictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
        [JsonPropertyName("tenant")]
        public string Tenant { get; set; } = string.Empty;
        [JsonPropertyName("database")]
        public string Database { get; set; } = string.Empty;
    }
}
