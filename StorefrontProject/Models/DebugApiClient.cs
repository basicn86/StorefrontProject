using StorefrontProject.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorefrontProject.Models
{
    public class DebugApiClient : IApiClient
    {
        public async Task<IEnumerable<NetworkResources.Product>> GetProductsAsync()
        {
            //generate a list of fake products for now
            List<NetworkResources.Product> products = new List<NetworkResources.Product>();
            products.Add(new NetworkResources.Product { Name = "Product 1", Price = 10.00m });
            products.Add(new NetworkResources.Product { Name = "Product 2", Price = 20.00m });
            products.Add(new NetworkResources.Product { Name = "Product 3", Price = 30.00m });

            //fake await
            await Task.Delay(100);

            //return the list of products for debugging
            return products.AsEnumerable();
        }

        //implement place order
        public async Task PlaceOrderAsync(NetworkResources.Order order)
        {
            //fake order placement
            await Task.Delay(1000);
        }

        //implement get orders
        public async Task<IEnumerable<NetworkResources.Order>> GetOrdersAsync()
        {
            //generate a list of fake orders for now
            List<NetworkResources.Order> orders = new List<NetworkResources.Order>();

            //fake await
            await Task.Delay(100);

            //return the list of orders for debugging
            return orders.AsEnumerable();
        }

        //placeholder for remove order
        public async Task RemoveOrderAsync(Guid orderId)
        {
            //fake order removal
            await Task.Delay(1000);
        }

        //placeholder for update order
        public async Task UpdateOrderAsync(NetworkResources.Order order)
        {
            //fake order update
            await Task.Delay(1000);
        }
    }
}
