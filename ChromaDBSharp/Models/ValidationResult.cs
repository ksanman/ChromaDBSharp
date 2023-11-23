using System.Collections.Generic;

namespace ChromaDBSharp.Models
{
    public class ValidationResult
    {
        public IEnumerable<string> Ids { get; set; }
        public IEnumerable<IEnumerable<float>>? Embeddings { get; set; }
        public IEnumerable<IDictionary<string, object>>? Metadatas { get; set; }
        public IEnumerable<string>? Documents { get; set; }
        public ValidationResult(IEnumerable<string> ids, IEnumerable<IEnumerable<float>>? embeddings, IEnumerable<IDictionary<string, object>>? metadatas, IEnumerable<string>? documents)
        {
            Ids = ids;
            Embeddings = embeddings;
            Metadatas = metadatas;
            Documents = documents;
        }
    }
}
