using System.Collections.Generic;

namespace ChromaDBSharp.Models
{
    public class GetResult
    {
        public IEnumerable<string>? Ids { get; set; } = null;
        public IEnumerable<IEnumerable<float>>? Embeddings { get; set; } = null;
        public IEnumerable<IDictionary<string, string>>? Metadatas { get; set; } = null;
        public IEnumerable<string>? Documents { get; set; } = null;
        public IEnumerable<string>? URIs { get; set; } = null;
        public IEnumerable<string>? Data { get; set; } = null;
    }
}
