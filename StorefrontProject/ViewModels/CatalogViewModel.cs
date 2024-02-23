using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    public class CatalogViewModel : ViewModelBase
    {
        public ObservableCollection<CatalogItemViewModel> CatalogItems { get; set; }
        
        //public shopping cart command
        public ReactiveCommand<Unit, Unit>? UpdateShoppingCart { get; set; }

        public CatalogViewModel()
        {
            CatalogItems = new ObservableCollection<CatalogItemViewModel>();

            //load the products and do not await for it
            _ = LoadProductsAsync();
        }

        private async Task LoadProductsAsync()
        {
            //if API service is null, return
            if (ApiService.Instance == null) return;

            //Get the list of products from the API
            IEnumerable<NetworkResources.Product> products = await ApiService.Instance.GetProductsAsync();

            //add the products to the catalog items
            foreach (var product in products)
            {
                CatalogItemViewModel catalogItem = new CatalogItemViewModel(product.Name, product.Price, null);

                catalogItem.AddToCartCommand = ReactiveCommand.Create(() =>
                {
                    ShoppingCartService.Instance?.AddItem(product, catalogItem.Quantity);
                    UpdateShoppingCart?.Execute().Subscribe();
                });

                CatalogItems.Add(catalogItem);
            }
        }
    }
}
