using Newtonsoft.Json;

namespace ChromaDBSharp.Models
{
    internal class HeartbeatResponse
    {
        [JsonProperty("nanosecond heartbeat")]
        public long Heartbeat { get; set; }
    }
}
