using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using ServerlessAPI.Entities;
using ServerlessAPI.Repositories;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace ServerlessTests;

public class ProductTests
{
    private readonly WebApplicationFactory<Program> webApplication;

    public ProductTests()
    {
        webApplication = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped<IProductRepository, MockProductRepository>();
                services.AddScoped<IOrderRepository, MockOrderRepository>();
                services.AddScoped<IOrderItemRepository, MockOrderItemRepository>();
            });
        });
    }

    //Note: for the purposes of this project, the products are hard coded, and new ones cannot be added
    [Fact]
    public async Task AddProductsTest()
    {
        var client = webApplication.CreateClient();

        HttpResponseMessage response = await client.PostAsync("/api/products", null);
        response.EnsureSuccessStatusCode();
        response = await client.GetAsync("/api/products?limit=10");
        response.EnsureSuccessStatusCode();

        var products = await response.Content.ReadFromJsonAsync<List<Product>>();
        Assert.NotNull(products);
        Assert.Equal(3, products.Count);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task GetProductsTest(int limit)
    {
        var client = webApplication.CreateClient();

        HttpResponseMessage response = await client.PostAsync("/api/products", null);
        response.EnsureSuccessStatusCode();
        response = await client.GetAsync($"/api/products?limit={limit}");
        response.EnsureSuccessStatusCode();

        var products = await response.Content.ReadFromJsonAsync<List<Product>>();
        Assert.NotNull(products);
        Assert.Equal(limit, products.Count);
    }
}
