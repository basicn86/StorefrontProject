using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace StorefrontProject.ViewModels
{
    public class ShoppingCartItemViewModel : ViewModelBase
    {
        private string? name;
        public string? Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        private decimal price;
        public decimal Price
        {
            get => price;
            set => this.RaiseAndSetIfChanged(ref price, value);
        }

        //price in string format
        public string PriceString
        {
            get
            {
                return price.ToString("C");
            }
            set { }
        }

        private uint quantity;
        public uint Quantity
        {
            get => quantity;
            set => this.RaiseAndSetIfChanged(ref quantity, value);
        }

        //Remove item from cart command
        public ReactiveCommand<Unit, Unit> RemoveItemCommand { get; set; }

        public decimal Total => price * Quantity;
    }
}
