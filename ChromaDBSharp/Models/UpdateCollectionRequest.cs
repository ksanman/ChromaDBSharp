using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ChromaDBSharp.Models
{
    public class UpdateCollectionRequest
    {
        [JsonPropertyName("new_name")]
        public string? NewName { get; set; } = null;
        [JsonPropertyName("new_metadata")]
        public IDictionary<string, object>? NewMetadata = null;
    }
}
