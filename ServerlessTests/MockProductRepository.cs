using ServerlessAPI.Entities;
using ServerlessAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerlessTests
{
    internal class MockProductRepository : IProductRepository
    {
        public static List<Product> products = new List<Product>();
        Task IProductRepository.AddProductsAsync(IList<Product> productsToAdd)
        {
            products.AddRange(productsToAdd);
            return Task.CompletedTask;
        }

        Task IProductRepository.DeleteAllProductsAsync()
        {
            products.Clear();
            return Task.CompletedTask;
        }

        Task<IList<Product>> IProductRepository.GetProductsAsync(int limit)
        {
            return Task.FromResult<IList<Product>>(products.Take(limit).ToList());
        }
    }
}
