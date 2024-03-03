using NetworkResources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.ReactiveUI;
using System.Reactive;
using ReactiveUI;

namespace StorefrontProject.ViewModels
{
    public class OrderViewModel
    {
        //order ID. This can be set to 0 on the client side. When placing an order, the server will assign a unique ID to the order, and disregard the client's ID.
        public int Id { get; set; }
        //total price of the order.
        public decimal TotalPrice { get; set; }
        public string TotalPriceString => TotalPrice.ToString("C");
        //date of placement
        public DateTime Date { get; set; }
        //date string in dd/MM/yyyy - HH:mm format
        public string DateString => Date.ToString("dd/MM/yyyy - HH:mm");

        //list of order items.
        public ObservableCollection<OrderItem> OrderItems { get; set; }

        public OrderViewModel()
        {
            OrderItems = new ObservableCollection<OrderItem>();
        }

        //Generate an OrderViewModel from an NetworkResources.Order
        public OrderViewModel(Order order)
        {
            Id = order.Id;
            TotalPrice = order.TotalPrice;
            Date = order.Date;
            OrderItems = new ObservableCollection<OrderItem>(order.OrderItems);
        }
    }
}
