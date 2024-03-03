using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorefrontProject.Models.Interfaces
{
    public interface IApiClient
    {
        public Task<IEnumerable<NetworkResources.Product>> GetProductsAsync();

        //place order
        public Task PlaceOrderAsync(NetworkResources.OrderRequest orderRequest);

        //get orders
        public Task<IEnumerable<NetworkResources.Order>> GetOrdersAsync();

        //remove order
        public Task RemoveOrderAsync(int orderId);
    }
}
