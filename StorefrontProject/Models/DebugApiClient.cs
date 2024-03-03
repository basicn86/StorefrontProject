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

            //return the list of products for debugging
            return products.AsEnumerable();
        }

        //implement place order
        public async Task PlaceOrderAsync(NetworkResources.OrderRequest orderRequest)
        {
            //fake order placement
            await Task.Delay(1000);
        }

        //implement get orders
        public async Task<IEnumerable<NetworkResources.Order>> GetOrdersAsync()
        {
            //generate a list of fake orders for now
            List<NetworkResources.Order> orders = new List<NetworkResources.Order>();
            orders.Add(new NetworkResources.Order { Id = 1, TotalPrice = 10.00m });
            orders.Add(new NetworkResources.Order { Id = 2, TotalPrice = 20.00m });
            orders.Add(new NetworkResources.Order { Id = 3, TotalPrice = 30.00m });

            //return the list of orders for debugging
            return orders.AsEnumerable();
        }

        //placeholder for remove order
        public async Task RemoveOrderAsync(int orderId)
        {
            //fake order removal
            await Task.Delay(1000);
        }
    }
}
