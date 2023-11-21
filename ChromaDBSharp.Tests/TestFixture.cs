using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromaDBSharp.Tests
{
    public class TestFixture : IDisposable
    {
        private readonly HttpClient _httpClient;
        public HttpClient Client => _httpClient;
        public TestFixture()
        {
            // Do "global" initialization here; Only called once.
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:8000/")
            };

        }

        public void Dispose()
        {
            // Do "global" teardown here; Only called once.
            _httpClient.Dispose();
        }
    }
}
