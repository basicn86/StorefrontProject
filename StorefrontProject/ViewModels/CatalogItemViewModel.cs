using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using ReactiveUI;

namespace StorefrontProject.ViewModels
{
    public class CatalogItemViewModel
    {
        public CatalogItemViewModel(string name, decimal price, Bitmap? productImage)
        {
            Name = name;
            Price = price;
            ProductImage = productImage;
            AddToCartCommand = ReactiveCommand.Create(() =>
            {

            });
        }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Bitmap? ProductImage { get; set; }
        public int Quantity { get; set; }
        public ReactiveCommand<Unit, Unit>? AddToCartCommand { get; set; }
    }
}
