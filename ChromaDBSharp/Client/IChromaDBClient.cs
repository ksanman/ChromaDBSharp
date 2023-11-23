using ChromaDBSharp.Embeddings;
using ChromaDBSharp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChromaDBSharp.Client
{
    public interface IChromaDBClient
    {
        /// <summary>
        /// Resets the database. This will delete all collections and entries.
        /// </summary>
        /// <returns>True if the database was reset successfully.</returns>
        bool Reset();
        /// <summary>
        /// Resets the database. This will delete all collections and entries.
        /// </summary>
        /// <returns>True if the database was reset successfully.</returns>
        Task<bool> ResetAsync();
        /// <summary>
        /// Get the version of Chroma.
        /// </summary>
        /// <returns>The version of Chroma</returns>
        string Version();
        /// <summary>
        /// Get the version of Chroma.
        /// </summary>
        /// <returns>The version of Chroma</returns>
        Task<string> VersionAsync();
        /// <summary>
        /// Get the current time in nanoseconds since epoch. Used to check if the server is alive.
        /// </summary>
        /// <returns>The current time in nanoseconds since epoch</returns>
        long Heartbeat();
        /// <summary>
        /// Get the current time in nanoseconds since epoch. Used to check if the server is alive.
        /// </summary>
        /// <returns>The current time in nanoseconds since epoch</returns>
        Task<long> HeartbeatAsync();
        /// <summary>
        /// List all collections.
        /// </summary>
        /// <returns>A list of collections</returns>
        IEnumerable<Collection> ListCollections();
        /// <summary>
        /// List all collections.
        /// </summary>
        /// <returns>A list of collections</returns>
        Task<IEnumerable<Collection>> ListCollectionsAsync();

        /// <summary>
        /// Create a new collection with the given name and metadata.
        /// </summary>
        /// <param name="name">The name of the collection to create.</param>
        /// <param name="metadata">Optional metadata to associate with the collection.</param>
        /// <param name="embeddingFunction">Optional custom embedding function for the collection.</param>
        /// <param name="createOrGet">If True, return the existing collection if it exists.</param>
        /// <returns>The newly created collection.</returns>
        ICollectionClient CreateCollection(string name, IDictionary<string, object>? metadata = null, IEmbeddable? embeddingFunction = null, bool createOrGet = false);

        /// <summary>
        /// Create a new collection with the given name and metadata.
        /// </summary>
        /// <param name="name">The name of the collection to create.</param>
        /// <param name="metadata">Optional metadata to associate with the collection.</param>
        /// <param name="embeddingFunction">Optional custom embedding function for the collection.</param>
        /// <param name="createOrGet">If True, return the existing collection if it exists.</param>
        /// <returns>The newly created collection.</returns>
        Task<ICollectionClient> CreateCollectionAsync(string name, IDictionary<string, object>? metadata = null, IEmbeddable? embeddingFunction = null, bool createOrGet = false);
        /// <summary>
        /// Get a collection with the given name.
        /// </summary>
        /// <param name="name">The name of the collection to get</param>
        /// <param name="embeddingFunction">Optional custom embedding function for the collection.</param>
        /// <returns>The collection</returns>
        ICollectionClient GetCollection(string name, IEmbeddable? embeddingFunction = null);
        /// <summary>
        /// Get a collection with the given name.
        /// </summary>
        /// <param name="name">The name of the collection to get</param>
        /// <param name="embeddingFunction">Optional custom embedding function for the collection.</param>
        /// <returns>The collection</returns>
        Task<ICollectionClient> GetCollectionAsync(string name, IEmbeddable? embeddingFunction = null);
        /// <summary>
        /// Delete a collection with the given name.
        /// </summary>
        /// <param name="name">The name of the collection to delete.</param>
        void DeleteCollection(string name);
        /// <summary>
        /// Delete a collection with the given name.
        /// </summary>
        /// <param name="name">The name of the collection to delete.</param>
        Task DeleteCollectionAsync(string name);
        /// <summary>
        /// Updates a collection given the id.
        /// </summary>
        /// <param name="collectionId">Id of the collection to update.</param>
        /// <param name="name">Optional new collection name.</param>
        /// <param name="metadata">Optional new collection metadata.</param>
        void UpdateCollection(string collectionId, string? name = null, IDictionary<string, object>? metadata = null);
        /// <summary>
        /// Updates a collection given the id.
        /// </summary>
        /// <param name="request">Object that contain the collection id, and the new name and/or new metadata to add.</param>
        Task UpdateCollectionAsync(string collectionId, string? name = null, IDictionary<string, object>? metadata = null);
    }
}
