using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

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
                services.AddControllers();
            });
        });
    }

    [Fact]
    public void Test1()
    {

    }
}
