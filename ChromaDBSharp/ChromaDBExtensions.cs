using ChromaDBSharp.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace ChromaDBSharp
{
    public static class ChromaDBExtensions
    {
        public static void RegisterChromaDBSharp(this IServiceCollection services, string url)
        {
            services.RegisterChromaDBSharp(client => client.BaseAddress = new Uri(url));
        }

        public static void RegisterChromaDBSharp(this IServiceCollection services, Action<HttpClient> configureClient)
        {
            services.AddScoped<IChromaDBClient, ChromaDBClient>();
            services.AddHttpClient<IChromaDBClient, ChromaDBClient>(configureClient);
        }
    }
}
