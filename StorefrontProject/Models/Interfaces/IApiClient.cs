using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorefrontProject.Models.Interfaces
{
    public interface IApiClient
    {
        /// <summary>
        /// Asynchronously retrieves a collection of products from the network.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an IEnumerable of Products</returns>
        public Task<IEnumerable<NetworkResources.Product>> GetProductsAsync();

        /// <summary>
        /// Asynchronously places an order to the network resource.
        /// </summary>
        /// <param name="orderRequest">The order request to be placed.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task PlaceOrderAsync(NetworkResources.OrderRequest orderRequest);

        //get orders
        public Task<IEnumerable<NetworkResources.Order>> GetOrdersAsync();

        /// <summary>
        /// Asynchronously removes an order from the network resource.
        /// </summary>
        /// <param name="orderId">The ID of the order to be removed.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task RemoveOrderAsync(int orderId);

        public Task UpdateOrderAsync(NetworkResources.Order order);
    }
}
