using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows.Input;
using ReactiveUI;
using NetworkResources;

namespace StorefrontProject.ViewModels
{
    public class OrderViewModel : ViewModelBase
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

            //reactive cancel order button
            CancelOrderCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var vm = new ConfirmDialogViewModel("Are you sure you want to cancel this order?");

                var result = await Services.DialogService.ConfirmDialogInteraction.Handle(vm);

                //print out to console if not null
                if (result != null && result == true)
                {
                    NotifyOrderCancelled?.Execute(this).Subscribe();
                }
            });
        }

        //reactive cancel order button
        public ReactiveCommand<Unit, Unit> CancelOrderCommand { get; set; }

        //reactive command to notify the main window that the order has been cancelled
        public ReactiveCommand<OrderViewModel, Unit>? NotifyOrderCancelled { get; set; }
    }
}
