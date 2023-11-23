using System;
using System.Collections.Generic;
using System.Text;

namespace ChromaDBSharp.Embeddings
{
    public interface IEmbeddable
    {
        public float[][] Generate(string[] texts);
    }
}
