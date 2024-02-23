using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StorefrontProject.Models.Interfaces;

namespace StorefrontProject.Models
{
    public class DebugShoppingCart : IShoppingCart
    {
        private Dictionary<NetworkResources.Product, uint> items = new Dictionary<NetworkResources.Product, uint>();

        //on constructor, add some items to the cart
        public DebugShoppingCart()
        {
            items.Add(new NetworkResources.Product { Id = 1, Name = "Product 1", Price = 10 }, 25);
            items.Add(new NetworkResources.Product { Id = 2, Name = "Product 2", Price = 20 }, 30);
            items.Add(new NetworkResources.Product { Id = 3, Name = "Product 3", Price = 30 }, 60);
        }

        public void AddItem(NetworkResources.Product product, uint quantity)
        {
            if (items.ContainsKey(product))
            {
                items[product] += quantity;
            }
            else
            {
                items.Add(product, quantity);
            }
        }

        public void Clear()
        {
            items.Clear();
        }

        public Dictionary<NetworkResources.Product, uint> GetItems()
        {
            return items;
        }

        public void RemoveItem(NetworkResources.Product product)
        {
            items.Remove(product);
        }

        public void UpdateQuantity(NetworkResources.Product product, uint quantity)
        {
            items[product] = quantity;
        }
        
        //fake order placement
        public async Task Checkout()
        {
            await Task.Delay(1000);
        }
    }
}
