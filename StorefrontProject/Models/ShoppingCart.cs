using ReactiveUI;
using StorefrontProject.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace StorefrontProject.Models
{
    //derive from IShoppingCart
    public class ShoppingCart : IShoppingCart
    {
        //dictionary of products and their quantities
        private Dictionary<NetworkResources.Product, uint> items = new Dictionary<NetworkResources.Product, uint>();

        //Add an item to the shopping cart
        public void AddItem(NetworkResources.Product product, uint quantity)
        {
            //if the product is already in the cart, add the quantity to the existing quantity
            if (items.ContainsKey(product))
            {
                items[product] += quantity;
            }
            //otherwise, add the product to the cart
            else
            {
                items.Add(product, quantity);
            }
        }

        //Get all items in the shopping cart with their quantities
        public Dictionary<NetworkResources.Product, uint> GetItems()
        {
            return items;
        }

        //Remove an item from the shopping cart
        public void RemoveItem(NetworkResources.Product product)
        {
            items.Remove(product);
        }

        //update the quantity of an item in the shopping cart
        public void UpdateQuantity(NetworkResources.Product product, uint quantity)
        {
            items[product] = quantity;
        }

        //clear the shopping cart
        public void Clear()
        {
            items.Clear();
        }

        //checkout the shopping cart
        public async Task Checkout()
        {
            //if the cart is empty, return
            if (items.Count == 0) return;
            //if the API is null, return
            if (ApiService.Instance == null) return;

            //convert the dictionary to a dictionary of product IDs to quantities to be interpretable by the API
            Dictionary<int, int> orderItems = new Dictionary<int, int>();
            decimal TotalPrice = 0;

            foreach (var item in items)
            {
                orderItems.Add(item.Key.Id, (int)item.Value);
                TotalPrice += item.Key.Price * item.Value;
            }

            //create a new OrderRequest
            NetworkResources.OrderRequest orderRequest = new NetworkResources.OrderRequest
            {
                OrderItems = orderItems,
                TotalPrice = TotalPrice
            };

            //send the order request to the API
            await ApiService.Instance?.PlaceOrderAsync(orderRequest);

            //clear the cart
            items.Clear();
        }
    }
}
