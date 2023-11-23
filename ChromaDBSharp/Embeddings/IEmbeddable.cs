using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChromaDBSharp.Embeddings
{
    public interface IEmbeddable
    {
        Task<IEnumerable<IEnumerable<float>>> Generate(IEnumerable<string> texts);
    }
}
