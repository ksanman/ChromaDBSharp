using ChromaDBSharp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChromaDBSharp.Client
{
    public interface ICollectionClient
    {
        void Add(IEnumerable<string> ids, IEnumerable<IEnumerable<float>>? embeddings, IDictionary<string, object> metadatas, IEnumerable<string> documents);
        Task AddAsync(IEnumerable<string> ids, IEnumerable<IEnumerable<float>>? embeddings, IDictionary<string, object> metadatas, IEnumerable<string> documents);
        void Update(IEnumerable<string> ids, IEnumerable<IEnumerable<float>>? embeddings, IDictionary<string, object> metadatas, IEnumerable<string> documents);
        Task UpdateAsync(IEnumerable<string> ids, IEnumerable<IEnumerable<float>>? embeddings, IDictionary<string, object> metadatas, IEnumerable<string> documents);
        void Upsert(IEnumerable<string> ids, IEnumerable<IEnumerable<float>>? embeddings, IDictionary<string, object> metadatas, IEnumerable<string> documents);
        Task UpsertAsync(IEnumerable<string> ids, IEnumerable<IEnumerable<float>>? embeddings, IDictionary<string, object> metadatas, IEnumerable<string> documents);
        GetResult Get(IEnumerable<string>? ids = null, IDictionary<string, object>? where = null,int? limit = null,int? offset = null,IDictionary<string, object>? whereDocument = null,IEnumerable<string>? include = null);
        Task<GetResult> GetAsync(IEnumerable<string>? ids = null, IDictionary<string, object>? where = null, int? limit = null, int? offset = null, IDictionary<string, object>? whereDocument = null, IEnumerable<string>? include = null);
        void Delete(IEnumerable<string>? ids = null, IDictionary<string, object>? where = null, IDictionary<string, object>? whereDocument = null);
        Task DeleteAsync(IEnumerable<string>? ids = null, IDictionary<string, object>? where = null, IDictionary<string, object>? whereDocument = null);
        int Count();
        Task<int> CountAsync();
        QueryResult Query(IDictionary<string, object>? where,IDictionary<string, object>? whereDocument, IEnumerable<IEnumerable<float>>? queryEmbeddings, int numberOfResults = 10,IEnumerable<string>? include = null);
        Task<QueryResult> QueryAsync(IDictionary<string, object>? where, IDictionary<string, object>? whereDocument, IEnumerable<IEnumerable<float>>? queryEmbeddings, int numberOfResults = 10, IEnumerable<string>? include = null);
    }
}
