using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using ReactiveUI;
using StorefrontProject.Models;
using StorefrontProject.Models.Interfaces;

namespace StorefrontProject.ViewModels
{
    public class CatalogItemViewModel : ViewModelBase
    {
        public CatalogItemViewModel(string name, decimal price, Bitmap? productImage, ReactiveCommand<Unit, Unit> notifyCartUpdate, NetworkResources.Product product)
        {
            Name = name;
            Price = price;
            ProductImage = productImage;
            Quantity = 1;
            this.Product = product;

            AddToCartCommand = ReactiveCommand.Create(() =>
            {
                ShoppingCartService.Instance?.AddItem(Product, Quantity);
                notifyCartUpdate.Execute().Subscribe();
            });
        }
        //product
        public NetworkResources.Product Product { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Bitmap? ProductImage { get; set; }
        private uint quantity;
        public uint Quantity
        {
            get => quantity;
            set => this.RaiseAndSetIfChanged(ref quantity, value);
        }

        //background color
        public string BackgroundColor { get; set; } = "#FFFFFF";
        public ReactiveCommand<Unit, Unit>? AddToCartCommand { get; set; }
    }
}
