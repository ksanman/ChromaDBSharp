using ChromaDBSharp.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace ChromaDBSharp
{
    public static class ChromaDBExtensions
    {
        public static void RegisterChromaDBSharp(this IServiceCollection services)
        {
            services.AddHttpClient<IChromaDBClient, ChromaDBClient>();
            services.AddScoped<IChromaDBClient, ChromaDBClient>();
        }

        public static void RegisterChromaDBSharp(this IServiceCollection services, Action<HttpClient> configureClient)
        {
            services.AddHttpClient<IChromaDBClient, ChromaDBClient>(configureClient);
            services.AddScoped<IChromaDBClient, ChromaDBClient>();
        }
    }
}
