using System;
using Xunit;

namespace kconnected.IntegrationTests
{
    public class IntegrationTests
    {
        protected readonly HttpClient _client;

        public IntegrationTests()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _client = appFactory.CreateClient();
        }
        
    }
}
