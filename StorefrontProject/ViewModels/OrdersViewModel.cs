using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.ReactiveUI;
using ReactiveUI;
using StorefrontProject.Models;
using System.Reactive;

namespace StorefrontProject.ViewModels
{
    public class OrdersViewModel : ViewModelBase
    {
        //observable collection of orders
        public ObservableCollection<OrderViewModel> OrderList { get; set; }

        private string msg;
        public string Msg
        {
            get => msg;
            set => this.RaiseAndSetIfChanged(ref msg, value);
        }
        public bool MsgVisible => !string.IsNullOrEmpty(Msg);

        public OrdersViewModel()
        {
            //initialize the observable collection
            OrderList = new ObservableCollection<OrderViewModel>();
            //get the orders from the server
            _ = GetOrders();
        }

        private async Task GetOrders()
        {
            //Set the message to "Loading orders..."
            Msg = "Loading orders...";

            //check if ApiService is initialized
            if (ApiService.Instance == null) return;

            //get the orders from the server
            var orders = await ApiService.Instance.GetOrdersAsync();

            //put the orders in the observable collection
            foreach (var order in orders)
            {
                //Convert the order to an OrderViewModel
                var orderViewModel = new OrderViewModel(order);

                //Inject the notify order cancelled command
                orderViewModel.NotifyOrderCancelled = ReactiveCommand.Create<OrderViewModel, Unit>(order =>
                {
                    _ = CancelOrder(order);
                    return Unit.Default;
                });

                //add the order to the observable collection
                OrderList.Add(orderViewModel);
            }

            //Set the message to an empty string
            Msg = "";
        }

        public async Task CancelOrder(OrderViewModel order)
        {
            //Set the message to "Cancelling order..."
            Msg = "Cancelling order...";

            //try to remove the order from the server
            try
            {
                await ApiService.Instance.RemoveOrderAsync(order.Id);
                //Set the message to an empty string
                Msg = "";
            }
            catch (Exception e)
            {
                //Set the message to an error message
                Msg = "Cancel Failed : " + e.Message;
                return;
            }

            //tell the user the orders are loading
            Msg = "Loading orders...";

            //reload the orders
            OrderList.Clear();
            await GetOrders();
        }
    }
}
