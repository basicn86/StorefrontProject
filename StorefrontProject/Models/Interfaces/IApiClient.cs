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
    }
}
