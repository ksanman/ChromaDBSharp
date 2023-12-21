using ChromaDBSharp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChromaDBSharp.Client
{
    public interface ICollectionClient
    {
        /// <summary>
        /// Add embeddings to the data store.
        /// </summary>
        /// <param name="ids">The ids of the embeddings you wish to add</param>
        /// <param name="embeddings">The embeddings to add. If None, embeddings will be computed based on the documents using the embedding_function set for the Collection. Optional.</param>
        /// <param name="metadatas">The metadata to associate with the embeddings. When querying, you can filter on this metadata. Optional.</param>
        /// <param name="documents">The documents to associate with the embeddings. Optional.</param>
        void Add(IEnumerable<string>? ids = null, IEnumerable<IEnumerable<float>>? embeddings = null, IEnumerable<IDictionary<string, object>>? metadatas = null, IEnumerable<string>? documents = null);
        /// <summary>
        /// Add embeddings to the data store.
        /// </summary>
        /// <param name="ids">The ids of the embeddings you wish to add</param>
        /// <param name="embeddings">The embeddings to add. If None, embeddings will be computed based on the documents using the embedding_function set for the Collection. Optional.</param>
        /// <param name="metadatas">The metadata to associate with the embeddings. When querying, you can filter on this metadata. Optional.</param>
        /// <param name="documents">The documents to associate with the embeddings. Optional.</param>
        Task AddAsync(IEnumerable<string>? ids = null, IEnumerable<IEnumerable<float>>? embeddings = null, IEnumerable<IDictionary<string, object>>? metadatas = null, IEnumerable<string>? documents = null);
        /// <summary>
        /// Update the embeddings, metadatas or documents for provided ids.
        /// </summary>
        /// <param name="ids">The ids of the embeddings to update</param>
        /// <param name="embeddings">The embeddings to add. If None, embeddings will be computed based on the documents using the embedding_function set for the Collection. Optional.</param>
        /// <param name="metadatas">The metadata to associate with the embeddings. When querying, you can filter on this metadata. Optional.</param>
        /// <param name="documents">The documents to associate with the embeddings. Optional.</param>
        void Update(IEnumerable<string>? ids = null, IEnumerable<IEnumerable<float>>? embeddings = null, IEnumerable<IDictionary<string, object>>? metadatas = null, IEnumerable<string>? documents = null);
        /// <summary>
        /// Update the embeddings, metadatas or documents for provided ids.
        /// </summary>
        /// <param name="ids">The ids of the embeddings to update</param>
        /// <param name="embeddings">The embeddings to add. If None, embeddings will be computed based on the documents using the embedding_function set for the Collection. Optional.</param>
        /// <param name="metadatas">The metadata to associate with the embeddings. When querying, you can filter on this metadata. Optional.</param>
        /// <param name="documents">The documents to associate with the embeddings. Optional.</param>
        Task UpdateAsync(IEnumerable<string>? ids = null, IEnumerable<IEnumerable<float>>? embeddings = null, IEnumerable<IDictionary<string, object>>? metadatas = null, IEnumerable<string>? documents = null);
        /// <summary>
        /// Update the embeddings, metadatas or documents for provided ids, or create them if they don't exist.
        /// </summary>
        /// <param name="ids">The ids of the embeddings to update</param>
        /// <param name="embeddings">The embeddings to add. If None, embeddings will be computed based on the documents using the embedding_function set for the Collection. Optional.</param>
        /// <param name="metadatas">The metadata to associate with the embeddings. When querying, you can filter on this metadata. Optional.</param>
        /// <param name="documents">The documents to associate with the embeddings. Optional.</param>
        void Upsert(IEnumerable<string>? ids = null, IEnumerable<IEnumerable<float>>? embeddings = null, IEnumerable<IDictionary<string, object>>? metadatas = null, IEnumerable<string>? documents = null);
        /// <summary>
        /// Update the embeddings, metadatas or documents for provided ids, or create them if they don't exist.
        /// </summary>
        /// <param name="ids">The ids of the embeddings to update</param>
        /// <param name="embeddings">The embeddings to add. If None, embeddings will be computed based on the documents using the embedding_function set for the Collection. Optional.</param>
        /// <param name="metadatas">The metadata to associate with the embeddings. When querying, you can filter on this metadata. Optional.</param>
        /// <param name="documents">The documents to associate with the embeddings. Optional.</param>
        Task UpsertAsync(IEnumerable<string>? ids = null, IEnumerable<IEnumerable<float>>? embeddings = null, IEnumerable<IDictionary<string, object>>? metadatas = null, IEnumerable<string>? documents = null);
        /// <summary>
        /// Get embeddings and their associate data from the data store. If no ids or where filter is provided returns all embeddings up to limit starting at offset.
        /// </summary>
        /// <param name="ids"> The ids of the embeddings to get. Optional.</param>
        /// <param name="where">A Where type dict used to filter results by. E.g. {"color" : "red", "price": 4.20}. Optional.</param>
        /// <param name="limit">The number of documents to return. Optional.</param>
        /// <param name="offset">The offset to start returning results from. Useful for paging results with limit. Optional.</param>
        /// <param name="whereDocument">A WhereDocument type dict used to filter by the documents. E.g. {$contains: {"text": "hello"}}. Optional.</param>
        /// <param name="include">A list of what to include in the results. Can contain "embeddings", "metadatas", "documents". Ids are always included. Defaults to ["metadatas", "documents"]. Optional.</param>
        /// <returns>A GetResult object containing the results.</returns>
        GetResult Get(IEnumerable<string>? ids = null, IDictionary<string, object>? where = null, int? limit = null, int? offset = null, IDictionary<string, object>? whereDocument = null, IEnumerable<string>? include = null);
        /// <summary>
        /// Get embeddings and their associate data from the data store. If no ids or where filter is provided returns all embeddings up to limit starting at offset.
        /// </summary>
        /// <param name="ids"> The ids of the embeddings to get. Optional.</param>
        /// <param name="where">A Where type dict used to filter results by. E.g. {"color" : "red", "price": 4.20}. Optional.</param>
        /// <param name="limit">The number of documents to return. Optional.</param>
        /// <param name="offset">The offset to start returning results from. Useful for paging results with limit. Optional.</param>
        /// <param name="whereDocument">A WhereDocument type dict used to filter by the documents. E.g. {$contains: {"text": "hello"}}. Optional.</param>
        /// <param name="include">A list of what to include in the results. Can contain "embeddings", "metadatas", "documents". Ids are always included. Defaults to ["metadatas", "documents"]. Optional.</param>
        /// <returns>A GetResult object containing the results.</returns>
        Task<GetResult> GetAsync(IEnumerable<string>? ids = null, IDictionary<string, object>? where = null, int? limit = null, int? offset = null, IDictionary<string, object>? whereDocument = null, IEnumerable<string>? include = null);
        /// <summary>
        /// Delete the embeddings based on ids and/or a where filter
        /// </summary>
        /// <param name="ids">The ids of the embeddings to delete</param>
        /// <param name="where">A Where type dict used to filter the delection by. E.g. {"color" : "red", "price": 4.20}. Optional.</param>
        /// <param name="whereDocument">A WhereDocument type dict used to filter the deletion by the document content. E.g. {$contains: {"text": "hello"}}. Optional.</param>
        void Delete(IEnumerable<string>? ids = null, IDictionary<string, object>? where = null, IDictionary<string, object>? whereDocument = null);
        /// <summary>
        /// Delete the embeddings based on ids and/or a where filter
        /// </summary>
        /// <param name="ids">The ids of the embeddings to delete</param>
        /// <param name="where">A Where type dict used to filter the delection by. E.g. {"color" : "red", "price": 4.20}. Optional.</param>
        /// <param name="whereDocument">A WhereDocument type dict used to filter the deletion by the document content. E.g. {$contains: {"text": "hello"}}. Optional.</param>
        Task DeleteAsync(IEnumerable<string>? ids = null, IDictionary<string, object>? where = null, IDictionary<string, object>? whereDocument = null);
        /// <summary>
        /// The total number of embeddings added to the database
        /// </summary>
        /// <returns>The total number of embeddings added to the database</returns>
        int Count();
        /// <summary>
        /// The total number of embeddings added to the database
        /// </summary>
        /// <returns>The total number of embeddings added to the database</returns>
        Task<int> CountAsync();
        /// <summary>
        /// Get the n_results nearest neighbor embeddings for provided query_embeddings or query_texts.
        /// </summary>
        /// <param name="where">A Where type dict used to filter results by. E.g. {"color" : "red", "price": 4.20}. Optional.</param>
        /// <param name="whereDocument">A WhereDocument type dict used to filter by the documents. E.g. {$contains: {"text": "hello"}}. Optional.</param>
        /// <param name="queryEmbeddings">The embeddings to get the closes neighbors of. Optional.</param>
        /// <param name="queryTexts">The document texts to get the closes neighbors of. Optional.</param>
        /// <param name="numberOfResults">The number of neighbors to return for each query_embedding or query_texts. Optional.</param>
        /// <param name="include">A list of what to include in the results. Can contain "embeddings", "metadatas", "documents", "distances". Ids are always included. Defaults to ["metadatas", "documents", "distances"]. Optional.</param>
        /// <returns> QueryResult object containing the results.</returns>
        QueryResult Query(IDictionary<string, object>? where = null, IDictionary<string, object>? whereDocument = null, IEnumerable<IEnumerable<float>>? queryEmbeddings = null, IEnumerable<string>? queryTexts = null, int numberOfResults = 10,IEnumerable<string>? include = null);
        /// <summary>
        /// Get the n_results nearest neighbor embeddings for provided query_embeddings or query_texts.
        /// </summary>
        /// <param name="where">A Where type dict used to filter results by. E.g. {"color" : "red", "price": 4.20}. Optional.</param>
        /// <param name="whereDocument">A WhereDocument type dict used to filter by the documents. E.g. {$contains: {"text": "hello"}}. Optional.</param>
        /// <param name="queryEmbeddings">The embeddings to get the closes neighbors of. Optional.</param>
        /// <param name="queryTexts">The document texts to get the closes neighbors of. Optional.</param>
        /// <param name="numberOfResults">The number of neighbors to return for each query_embedding or query_texts. Optional.</param>
        /// <param name="include">A list of what to include in the results. Can contain "embeddings", "metadatas", "documents", "distances". Ids are always included. Defaults to ["metadatas", "documents", "distances"]. Optional.</param>
        /// <returns> QueryResult object containing the results.</returns>
        Task<QueryResult> QueryAsync(IDictionary<string, object>? where = null, IDictionary<string, object>? whereDocument = null, IEnumerable<IEnumerable<float>>? queryEmbeddings = null, IEnumerable<string>? queryTexts = null, int numberOfResults = 10, IEnumerable<string>? include = null);
    }
}
