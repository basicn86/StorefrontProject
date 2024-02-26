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

        public CatalogViewModel(ReactiveCommand<Unit, Unit> updateShoppingCart)
        {
            CatalogItems = new ObservableCollection<CatalogItemViewModel>();
            UpdateShoppingCart = updateShoppingCart;

            //load the products and do not await for it
            _ = LoadProductsAsync();
        }

        private async Task LoadProductsAsync()
        {
            //if API service is null, return
            if (ApiService.Instance == null) return;

            //Get the list of products from the API
            IEnumerable<NetworkResources.Product> products = await ApiService.Instance.GetProductsAsync();

            bool OddItem = false;
            //add the products to the catalog items
            foreach (var product in products)
            {
                CatalogItemViewModel catalogItem = new CatalogItemViewModel(product.Name, product.Price, null, UpdateShoppingCart, product);

                //set the background color if the item is odd
                if (OddItem) catalogItem.BackgroundColor = "#F0F0F0";
                OddItem = !OddItem;

                CatalogItems.Add(catalogItem);
            }
        }
    }
}
