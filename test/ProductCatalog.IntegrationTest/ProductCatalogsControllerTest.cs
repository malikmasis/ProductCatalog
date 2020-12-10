using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ProductCatalog.WebApi;
using ProductCatalog.WebApi.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ProductCatalog.IntegrationTest
{
    public class ProductCatalogsControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public ProductCatalogsControllerTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/api/productcatalogs/getall")]
        public async Task TestGetAll(string url)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            client.Dispose();
            response.Content.Dispose();
        }

        [Theory]
        [InlineData("/api/productcatalogs/get/-1")]
        public async Task TestGetById(string url)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);

            Assert.False(response.IsSuccessStatusCode);
            client.Dispose();
            response.Content.Dispose();
        }
    }
}
