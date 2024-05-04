using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using ServerlessAPI.Entities;
using ServerlessAPI.Repositories;
using System.Net;
using System.Net.Http.Json;

namespace ServerlessTests;

public class OrderTests
{
    private readonly WebApplicationFactory<Program> webApplication;

    public OrderTests()
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

    //Tests the POST, GET, and DELETE methods for the Order
    [Fact]
    public async Task DeleteOrderTest()
    {
        var client = webApplication.CreateClient();

        var order = new Order
        {
            Id = Guid.NewGuid(),
            Date = DateTime.Now.ToString(),
            Price = 0,
            OrderItems = new List<OrderItem>()
        };

        //add an item to the order
        order.OrderItems.Add(new OrderItem
        {
            Id = Guid.NewGuid(),
            OrderId = order.Id,
            Quantity = 1
        });

        HttpResponseMessage response = await client.PostAsJsonAsync("/api/orders", order);
        response.EnsureSuccessStatusCode();

        //assert that the order was added
        response = await client.GetAsync("/api/orders");
        response.EnsureSuccessStatusCode();
        var responseOrders = await response.Content.ReadFromJsonAsync<List<Order>>();
        Assert.NotNull(responseOrders);
        Assert.Contains(responseOrders, x => x.Id == order.Id);

        response = await client.DeleteAsync($"/api/orders?id={order.Id}");
        response.EnsureSuccessStatusCode();

        response = await client.GetAsync("/api/orders");
        response.EnsureSuccessStatusCode();

        var orders = await response.Content.ReadFromJsonAsync<List<Order>>();
        Assert.NotNull(orders);
        Assert.DoesNotContain(orders, x => x.Id == order.Id);
    }

    //Tests the GET and POST methods for the Order
    [Fact]
    public async Task AddAndGetOrderTest()
    {
        var client = webApplication.CreateClient();

        var order = new Order
        {
            Id = Guid.NewGuid(),
            Date = DateTime.Now.ToString(),
            Price = 0,
            OrderItems = new List<OrderItem>()
        };

        //add an item to the order
        order.OrderItems.Add(new OrderItem
        {
            Id = Guid.NewGuid(),
            OrderId = order.Id,
            Quantity = 1
        });

        HttpResponseMessage response = await client.PostAsJsonAsync("/api/orders", order);
        response.EnsureSuccessStatusCode();
        response = await client.GetAsync("/api/orders");
        response.EnsureSuccessStatusCode();

        var ordersFromApi = await response.Content.ReadFromJsonAsync<List<Order>>();
        Assert.NotNull(ordersFromApi);
        var orderFromApi = ordersFromApi.FirstOrDefault(x => x.Id == order.Id);
        Assert.Equal(order.Id, orderFromApi.Id);
        Assert.Contains(orderFromApi.OrderItems, x => x.Id == order.OrderItems[0].Id);
    }

    //Tests the POST method for the Order without any items
    [Fact]
    public async Task AddOrderWithoutItemsTest()
    {
        var client = webApplication.CreateClient();

        var order = new Order
        {
            Id = Guid.NewGuid(),
            Date = DateTime.Now.ToString(),
            Price = 0,
            OrderItems = new List<OrderItem>()
        };

        HttpResponseMessage response = await client.PostAsJsonAsync("/api/orders", order);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    //Test the POST, PUT, and GET methods for the Order
    [Fact]
    public async Task UpdateOrderTest()
    {
        var client = webApplication.CreateClient();

        var order = new Order
        {
            Id = Guid.NewGuid(),
            Date = DateTime.Now.ToString(),
            Price = 0,
            OrderItems = new List<OrderItem>()
        };

        //add an item to the order
        order.OrderItems.Add(new OrderItem
        {
            Id = Guid.NewGuid(),
            OrderId = order.Id,
            Quantity = 1
        });

        HttpResponseMessage response = await client.PostAsJsonAsync("/api/orders", order);
        response.EnsureSuccessStatusCode();

        //assert that the order was added
        response = await client.GetAsync("/api/orders");
        response.EnsureSuccessStatusCode();
        var responseOrders = await response.Content.ReadFromJsonAsync<List<Order>>();
        Assert.NotNull(responseOrders);
        Assert.Contains(responseOrders, x => x.Id == order.Id);

        //update the order
        order.Price = 10;
        response = await client.PutAsJsonAsync("/api/orders", order);
        response.EnsureSuccessStatusCode();

        //assert that the order was updated
        response = await client.GetAsync("/api/orders");
        response.EnsureSuccessStatusCode();
        var orders = await response.Content.ReadFromJsonAsync<List<Order>>();
        Assert.NotNull(orders);
        var updatedOrder = orders.FirstOrDefault(x => x.Id == order.Id);
        Assert.NotNull(updatedOrder);
        Assert.Equal(10, updatedOrder.Price);
    }
}

