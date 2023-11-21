using ChromaDBSharp.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ChromaDBSharp
{
    public static class ChromaDBExtensions
    {
        public static void RegisterChromaDBSharp(this IServiceCollection services)
        {
            services.AddHttpClient<IChromaDBClient, ChromaDBClient>();
        }
    }
}
