using NetworkResources;
using StorefrontProject.Models.Interfaces;

namespace ClientTests
{
    class MockApiForShoppingCart : IApiClient
    {
        Task<IEnumerable<Order>> IApiClient.GetOrdersAsync()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Product>> IApiClient.GetProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Order? LastOrder = null;
        public bool ThrowException = false;
        Task IApiClient.PlaceOrderAsync(Order order)
        {
            if (ThrowException)
            {
                throw new Exception();
            }
            LastOrder = order;
            return Task.CompletedTask;
        }

        Task IApiClient.RemoveOrderAsync(Guid orderId)
        {
            throw new NotImplementedException();
        }

        Task IApiClient.UpdateOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }
    }

    [TestClass]
    public class ShoppingCartTest
    {
        [TestMethod]
        public void AddItemTest()
        {
            //inject the api to the main app
            StorefrontProject.Models.ApiService.Instance = new MockApiForShoppingCart();

            //create a new shopping cart
            IShoppingCart cart = new StorefrontProject.Models.ShoppingCart();

            cart.AddItem(new Product{ Id = Guid.NewGuid() }, 1);
            Assert.AreEqual(1, cart.GetItems().Count);

            cart.AddItem(new Product { Id = Guid.NewGuid() }, 2);
            Assert.AreEqual(2, cart.GetItems().Count);
        }

        [TestMethod]
        public void AddSameItemTwiceTest()
        {
            StorefrontProject.Models.ApiService.Instance = new MockApiForShoppingCart();
            IShoppingCart cart = new StorefrontProject.Models.ShoppingCart();

            Product testProduct = new Product { Id = Guid.NewGuid() };

            cart.AddItem(testProduct, 1);
            cart.AddItem(testProduct, 1);

            Assert.AreEqual(1, cart.GetItems().Count);
            Assert.AreEqual(2, (int)cart.GetItems()[testProduct]);
        }

        [TestMethod]
        public void RemoveItemTest()
        {

            StorefrontProject.Models.ApiService.Instance = new MockApiForShoppingCart();
            IShoppingCart cart = new StorefrontProject.Models.ShoppingCart();

            Product product = new Product();

            cart.AddItem(product, 1);
            cart.RemoveItem(product);

            Assert.AreEqual(0, cart.GetItems().Count);
        }

        [TestMethod]
        public void UpdateQuantityTest()
        {
            StorefrontProject.Models.ApiService.Instance = new MockApiForShoppingCart();
            IShoppingCart cart = new StorefrontProject.Models.ShoppingCart();

            Product product = new Product();

            cart.AddItem(product, 1);
            cart.UpdateQuantity(product, 2);

            Assert.AreEqual(2, (int)cart.GetItems()[product]);
        }

        [TestMethod]
        public void ClearTest()
        {
            StorefrontProject.Models.ApiService.Instance = new MockApiForShoppingCart();
            IShoppingCart cart = new StorefrontProject.Models.ShoppingCart();

            cart.AddItem(new Product { Id = Guid.NewGuid() }, 1);
            cart.AddItem(new Product { Id = Guid.NewGuid() }, 2);

            cart.Clear();
            Assert.AreEqual(0, cart.GetItems().Count);
        }

        [TestMethod]
        public void CheckoutTest()
        {
            MockApiForShoppingCart mockApi = new MockApiForShoppingCart();
            StorefrontProject.Models.ApiService.Instance = mockApi;
            IShoppingCart cart = new StorefrontProject.Models.ShoppingCart();

            Product testProduct = new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Product 1",
                Price = 10,
            };

            cart.AddItem(testProduct, 4);
            cart.Checkout();
            
            Assert.AreEqual(1, mockApi.LastOrder?.OrderItems.Count());
            Assert.AreEqual(4, mockApi.LastOrder?.OrderItems.First().Quantity);
            Assert.AreEqual(40, mockApi.LastOrder?.TotalPrice);
        }

        [TestMethod]
        public void CheckoutExceptionTest()
        {
            MockApiForShoppingCart mockApi = new MockApiForShoppingCart();
            mockApi.ThrowException = true;
            StorefrontProject.Models.ApiService.Instance = mockApi;
            IShoppingCart cart = new StorefrontProject.Models.ShoppingCart();

            Product testProduct = new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Product 1",
                Price = 10,
            };

            cart.AddItem(testProduct, 4);
            cart.Checkout();

            Assert.AreEqual(0, cart.GetItems().Count);
        }
    }
}