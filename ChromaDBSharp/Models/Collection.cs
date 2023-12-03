using Newtonsoft.Json;
using System.Collections.Generic;

namespace ChromaDBSharp.Models
{
    public class Collection
    {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;
        [JsonProperty("metadata")]
        public IDictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
        [JsonProperty("tenant")]
        public string Tenant { get; set; } = string.Empty;
        [JsonProperty("database")]
        public string Database { get; set; } = string.Empty;
    }
}
