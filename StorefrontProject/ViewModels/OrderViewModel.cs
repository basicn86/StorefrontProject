using NetworkResources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorefrontProject.ViewModels
{
    public class OrderViewModel
    {
        //order ID. This can be set to 0 on the client side. When placing an order, the server will assign a unique ID to the order, and disregard the client's ID.
        public int Id { get; set; }
        //total price of the order.
        public decimal TotalPrice { get; set; }
        public string TotalPriceString => TotalPrice.ToString("C");
        //list of order items.
        public ObservableCollection<OrderItem> OrderItems { get; set; }

        public OrderViewModel()
        {
            OrderItems = new ObservableCollection<OrderItem>();
        }
    }
}
