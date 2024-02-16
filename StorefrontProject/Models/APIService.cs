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
            //return empty ienumerable to avoid the error for now
            return Enumerable.Empty<Product>();
        }
    }
}
