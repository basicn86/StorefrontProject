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
using System.Reactive.Linq;

namespace StorefrontProject.ViewModels
{
    //This class is for the order view. It contains a list of orders.
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

                //Inject the order cancelled command
                orderViewModel.CancelOrderCommand = ReactiveCommand.CreateFromTask(async () =>
                {
                    var vm = new ConfirmDialogViewModel("Are you sure you want to cancel this order?");
                    var result = await Services.DialogService.ConfirmDialogInteraction.Handle(vm);
                    if (result == true)
                        _ = CancelOrder(orderViewModel.Id);
                    return Unit.Default;
                });

                orderViewModel.SaveChangesCommand = ReactiveCommand.CreateFromTask(async () =>
                {
                    var vm = new ConfirmDialogViewModel("Are you sure you want to save changes to this order?\n" +
                        "Your old total is " + orderViewModel.InitialTotalPriceString + "\n" +
                        "Your new total is " + orderViewModel.TotalPriceString);
                    var result = await Services.DialogService.ConfirmDialogInteraction.Handle(vm);
                    if (result == true) _ = SaveChanges(orderViewModel);
                    return Unit.Default;
                });

                //add the order to the observable collection
                OrderList.Add(orderViewModel);
            }

            //Set the message to an empty string
            Msg = "";
        }

        //Cancel order method, usually called by the order item to notify the OrdersViewModel to cancel the order and refresh the list
        public async Task CancelOrder(int Id)
        {
            //Set the message to "Cancelling order..."
            Msg = "Cancelling order...";

            //try to remove the order from the server
            try
            {
                await ApiService.Instance.RemoveOrderAsync(Id);
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

        //Method to save changes to an order
        public async Task SaveChanges(OrderViewModel orderViewModel)
        {
            //Set the message to "Saving changes..."
            Msg = "Saving changes...";

            //try to update the order on the server
            try
            {
                await ApiService.Instance.UpdateOrderAsync(orderViewModel.ToOrder());
                //Set the message to an empty string
                Msg = "";
            }
            catch (Exception e)
            {
                //Set the message to an error message
                Msg = "Save Failed : " + e.Message;
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
