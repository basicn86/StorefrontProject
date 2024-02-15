using StorefrontProject.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorefrontProject.Models
{
    public class APIService : IAPIService
    {
        //get products from the database
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            using (StoreDbContext context = new StoreDbContext("store.db"))
            {
                return await Task.Run(() => (from p in context.Products select p).Take(10));
            }
        }
    }
}
