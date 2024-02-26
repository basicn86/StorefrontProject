using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.ReactiveUI;
using StorefrontProject.Models;

namespace StorefrontProject.ViewModels
{
    public class OrdersViewModel : ViewModelBase
    {
        //observable collection of orders
        public ObservableCollection<NetworkResources.Order> OrderList { get; set; }

        public OrdersViewModel()
        {
            //initialize the observable collection
            OrderList = new ObservableCollection<NetworkResources.Order>();
            //get the orders from the server
            _ = GetOrders();
        }

        private async Task GetOrders()
        {
            //check if ApiService is initialized
            if (ApiService.Instance == null) return;

            //get the orders from the server
            var orders = await ApiService.Instance.GetOrdersAsync();

            //put the orders in the observable collection
            foreach (var order in orders)
            {
                OrderList.Add(order);
            }
        }
    }
}
