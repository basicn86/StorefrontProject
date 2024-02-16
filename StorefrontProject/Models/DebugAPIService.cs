using StorefrontProject.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorefrontProject.Models
{
    public class DebugAPIService : IAPIService
    {
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            //fake await for 1 seconds
            await Task.Delay(1000);

            //generate a list of fake products for now
            List<Product> products = new List<Product>();
            products.Add(new Product { Name = "Product 1", Price = 10.00m });
            products.Add(new Product { Name = "Product 2", Price = 20.00m });
            products.Add(new Product { Name = "Product 3", Price = 30.00m });

            //return the list of products for debugging
            return products.AsEnumerable();
        }
    }
}
