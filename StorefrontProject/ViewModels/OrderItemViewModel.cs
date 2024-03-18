using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive;
using ReactiveUI;
using NetworkResources;

namespace StorefrontProject.ViewModels
{
    //This class is for an item of an order. It contains the product ID, name, quantity, and price.
    public class OrderItemViewModel : ViewModelBase
    {
        //product ID
        private int id = 0;
        public int Id
        {
            get => id;
            set => this.RaiseAndSetIfChanged(ref id, value);
        }

        //product name
        private string name = "";
        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        //quantity private and public property
        private int quantity = 0;
        public int Quantity
        {
            get => quantity;
            set => this.RaiseAndSetIfChanged(ref quantity, value);
        }

        //product price, not settable
        private decimal price = 0;
        public decimal Price
        {
            get => price;
            set => this.RaiseAndSetIfChanged(ref price, value);
        }

        //PriceString will return the price as a currency string
        public string PriceString => Price.ToString("C");

        //empty constructor
        public OrderItemViewModel()
        {

        }

        //constructor that takes a NetworkResources.OrderItem and sets the properties in this view model
        public OrderItemViewModel(OrderItem orderItem)
        {
            Id = orderItem.OrderId;
            Name = orderItem.Name;
            Price = orderItem.Price;
            Quantity = orderItem.Quantity;
        }

        //constructor that allows cloning an OrderItemViewModel
        public OrderItemViewModel(OrderItemViewModel orderItem)
        {
            Id = orderItem.Id;
            Name = orderItem.Name;
            Price = orderItem.Price;
            Quantity = orderItem.Quantity;
        }
    }
}
