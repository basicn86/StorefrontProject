using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkResources
{
    public class Order
    {
        //order ID. This can be set to 0 on the client side. When placing an order, the server will assign a unique ID to the order, and disregard the client's ID.
        public int Id { get; set; }
        //total price of the order.
        public decimal TotalPrice { get; set; }
        //date of placement
        public DateTime Date { get; set; }
        //list of order items.
        public List<OrderItem> OrderItems { get; set; }

        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}
