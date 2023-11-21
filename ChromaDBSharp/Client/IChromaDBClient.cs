using ChromaDBSharp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChromaDBSharp.Client
{
    public interface IChromaDBClient
    {
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
        /// <param name="createOrGet">If True, return the existing collection if it exists.</param>
        /// <returns>The newly created collection.</returns>
        Collection CreateCollection(string name, IDictionary<string, object>? metadata = null, bool createOrGet = false);

        /// <summary>
        /// Create a new collection with the given name and metadata.
        /// </summary>
        /// <param name="name">The name of the collection to create.</param>
        /// <param name="metadata">Optional metadata to associate with the collection.</param>
        /// <param name="createOrGet">If True, return the existing collection if it exists.</param>
        /// <returns>The newly created collection.</returns>
        Task<Collection> CreateCollectionAsync(string name, IDictionary<string, object>? metadata = null, bool createOrGet = false);
        /// <summary>
        /// Get a collection with the given name.
        /// </summary>
        /// <param name="name">The name of the collection to get</param>
        /// <returns>The collection</returns>
        Collection GetCollection(string name);
        /// <summary>
        /// Get a collection with the given name.
        /// </summary>
        /// <param name="name">The name of the collection to get</param>
        /// <returns>The collection</returns>
        Task<Collection> GetCollectionAsync(string name);
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
        /// <param name="request">Object that contain the collection id, and the new name and/or new metadata to add.</param>
        void UpdateCollection(UpdateCollectionRequest request);
        /// <summary>
        /// Updates a collection given the id.
        /// </summary>
        /// <param name="request">Object that contain the collection id, and the new name and/or new metadata to add.</param>
        Task UpdateCollectionAsync(UpdateCollectionRequest request);
    }
}
