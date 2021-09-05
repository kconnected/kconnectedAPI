using System;
using Xunit;
using System.Net.Http;
using kconnected.API;
using Microsoft.AspNetCore.Mvc.Testing;

namespace kconnected.IntegrationTests
{
    public class IntegrationTests
    {
        protected readonly HttpClient _client;

        protected readonly string _URI = "https://localhost:5001/api";

        public IntegrationTests()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _client = appFactory.CreateClient();
        }


    }
}
