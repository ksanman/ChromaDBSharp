# ChromaDB Sharp
ChromaDBSharp is a wrapper around the Chroma API that exposes all functionality of that API to .NET. The project follows the ChromaDB Python and JavaScript client patterns.

Library is consumed as a .net standard 2.1 library.

## Nuget
[ChromaDBSharp](https://www.nuget.org/packages/ChromaDBSharp/0.0.1)

## How to Use

ChromaDB is designed to be used against a deployed version of ChromaDB. See [HERE](https://docs.trychroma.com/deployment) for official documentation on how to deploy ChromaDB. 

Each Chroma call features a syncronous and and asyncronous version.

```c\#
// Create your HttpClient and set the base address to the chroma instance
using HttpClient client = new();
client.BaseAddress = new Uri("http://localhost:8000/"); // 
Using local docker version for example.

ChromaDBClient client = new(httpClient);

// Additional options
ChromaDBClient client = new(httpClient, tenantName, databaseName);

string version = client.Version();
long heartbeat = await client.HeartbeatAsync();
```

- Creating a client using Dependency Injection.
```c\#
... //Create app builder
builder.Services.RegisterChromaDBSharp("http://localhost:8000/");

builder.Services.RegisterChromaDBSharp(client => {
// Configure HTTP client here. For example, add authentication.
client.BaseAddress = new Uri("http://localhost:8000/");
byte[] byteArray = Encoding.ASCII.GetBytes("username:password");
client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
});
```

- Creating a collection

Collections rquire an embedding function otherwise an error will through. Define an embedding class as follows:
```c\#
public sealed class CustomEmbedder : IEmbeddable
{
    public Task<IEnumerable<IEnumerable<float>>> Generate(IEnumerable<string> texts)
    {
        // Embedding logic here
        // For example, call an API, create custom c\# embedding logic, or use library. this is for demonstration only.
        ...
        return embeddings.
    }
}
```
For example, using [AllMiniLML6v2Sharp](https://github.com/ksanman/AllMiniLML6v2Sharp)

```c\#
public sealed class AllMiniEmbedder : IEmbeddable
{
    private readonly IEmbedder _embedder;
    public AllMiniEmbedding()
    {
        _embedder = new AllMiniLmL6V2Embedder(modelPath: "path/to/model", tokenizer: new AllMiniLmL6V2Sharp.Tokenizer.BertTokenizer("path/to/vocab"));
    }
    public async Task<IEnumerable<IEnumerable<float>>> Generate(IEnumerable<string> texts)
    {
        IEnumerable<IEnumerable<float>> result = _embedder.GenerateEmbeddings(texts);
        return await Task.FromResult(result);
    }
}
```

Pass into collection when fetching.

```c\#
IEmbeddable customEmbeddingFunction = new CustomEmbedder();
ICollectionClient collection = client.CreateCollection("Collection Name", metadata: new Dictionary<string, object> { {"prop1", "value 1"},{"prop2",2}}, embeddingFunction: customEmbeddingFunction);
```
- Add documents
```c\#
collection.Add(documents: new[] { "This is document 1", "This is document 2" }, metadatas: new[] { new Dictionary<string, object> { { "source", "notion" } }, new Dictionary<string, object> { { "source", "google-docs" } } }, ids: new[] { "doc 1", "doc 2" });
```
- Query Documents
```c\#
QueryResult result = collection.Query(queryTexts: new[] { "This is a query document" }, numberOfResults: 5);
            
```

- Query Documents with a where clause.
```c\#
QueryResult result = collection.Query(queryTexts: new[] { "This is a query document" }, where: new Dictionary<string, object> {{"source", "notion"}},  numberOfResults: 5);
            
```

## Example Applications
- Retrieval-Augmented Generation
    - See [This Project](https://github.com/ksanman/LlmTestApp) for an example of how to use ChromaDBSharp with [LlamaSharp](https://github.com/SciSharp/LLamaSharp) and [AllMiniLML6v2Sharp](https://github.com/ksanman/AllMiniLML6v2Sharp) for a GPT style RAG app.
