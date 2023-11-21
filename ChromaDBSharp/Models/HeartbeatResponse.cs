using System.Text.Json.Serialization;

namespace ChromaDBSharp.Models
{
    internal class HeartbeatResponse
    {
        [JsonPropertyName("nanosecond heartbeat")]
        public long Heartbeat { get; set; }
    }
}
