using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorefrontProject.Models.Interfaces
{
    public interface IShoppingCart
    {
        //Add an item to the shopping cart
        public void AddItem(NetworkResources.Product product, uint quantity);

        //Get all items in the shopping cart with their quantities
        public Dictionary<NetworkResources.Product, uint> GetItems();

        //Remove an item from the shopping cart
        public void RemoveItem(NetworkResources.Product product);

        //update the quantity of an item in the shopping cart
        public void UpdateQuantity(NetworkResources.Product product, uint quantity);

        //clear the shopping cart
        public void Clear();

        //checkout the shopping cart
        public Task Checkout();
    }
}
