using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows.Input;
using DynamicData;
using ReactiveUI;

namespace StorefrontProject.ViewModels
{
    //This class is for the order view. It contains a list of order items, and a cancel order button.
    public class OrderViewModel : ViewModelBase
    {
        //This can be set to 0 on the client side. When placing an order, the server will assign a unique ID to the order, and disregard the client's ID.
        public int Id { get; set; }
        
        //TotalPrice, it is determined by total price of all order items, calculated at runtime
        public decimal TotalPrice
        {
            get {
                try
                {
                    return OrderItems.Sum(x => x.Price * x.Quantity);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
        public string TotalPriceString => TotalPrice.ToString("C");
        public string InitialTotalPriceString { get; set; }

        //date of placement
        public DateTime Date { get; set; }
        public string DateString => Date.ToString("dd/MM/yyyy - HH:mm");

        private bool showDetails = false;
        public bool ShowDetails
        {
            get => showDetails;
            set => this.RaiseAndSetIfChanged(ref showDetails, value);
        }

        public ObservableCollection<OrderItemViewModel> OrderItems { get; set; }
        //list of original order items, used to cancel changes
        public ObservableCollection<OrderItemViewModel> OriginalOrderItems { get; set; }

        //Generate an OrderViewModel from an NetworkResources.Order
        public OrderViewModel(NetworkResources.Order order)
        {
            Id = order.Id;
            Date = order.Date;
            OrderItems = new ObservableCollection<OrderItemViewModel>();
            OriginalOrderItems = new ObservableCollection<OrderItemViewModel>();
            foreach (var item in order.OrderItems)
            {
                //The reason why we create two instances of OrderItemViewModel is because we need to keep track of the original order items, so we can cancel changes.
                //If we were to add the same instance to both lists, then changing the OrderItems list would also change the OriginalOrderItems list, defeating the purpose of the OriginalOrderItems list.
                OrderItems.Add(new OrderItemViewModel(item));
                OriginalOrderItems.Add(new OrderItemViewModel(item));
            }

            CreateReactiveCommands();

            //set the initial total price string
            InitialTotalPriceString = TotalPriceString;
        }

        private void CreateReactiveCommands()
        {
            //reactive command to show details
            ShowDetailsCommand = ReactiveCommand.Create(() =>
            {
                ShowDetails = !ShowDetails;
            });

            //cancel changes button, reactive command, clears the OrderItems list and adds the items from the original order
            CancelChangesCommand = ReactiveCommand.Create(() =>
            {
                OrderItems.Clear();
                //Clone the original order items
                foreach (var item in OriginalOrderItems)
                {
                    OrderItems.Add(new OrderItemViewModel(item));
                }
            });
        }

        //Method to convert the OrderViewModel to a NetworkResources.Order
        public NetworkResources.Order ToOrder()
        {
            var order = new NetworkResources.Order
            {
                Id = Id,
                Date = Date,
                OrderItems = new System.Collections.Generic.List<NetworkResources.OrderItem>(),
                TotalPrice = TotalPrice
            };
            foreach (var item in OrderItems)
            {
                order.OrderItems.Add(new NetworkResources.OrderItem
                {
                    //copy the properties from the OrderItemViewModel to the OrderItem
                    ProductId = item.ProductId,
                    Name = item.Name,
                    Quantity = item.Quantity,
                    Price = item.Price 
                });
            }
            return order;
        }

        #region ReactiveCommands
        public ReactiveCommand<Unit, Unit> CancelOrderCommand { get; set; }
        public ReactiveCommand<Unit, Unit> ShowDetailsCommand { get; set; }

        public ReactiveCommand<Unit, Unit> SaveChangesCommand { get; set; }

        public ReactiveCommand<Unit, Unit> CancelChangesCommand { get; set; }
        #endregion
    }
}
