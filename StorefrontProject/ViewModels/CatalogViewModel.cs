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

        private IAPIService apiService;
        private IShoppingCart shoppingCart;
        
        //public shopping cart command
        public ReactiveCommand<Unit, Unit>? UpdateShoppingCart { get; set; }

        public CatalogViewModel(IAPIService _apiService, IShoppingCart shoppingCart)
        {
            CatalogItems = new ObservableCollection<CatalogItemViewModel>();
            apiService = _apiService;
            this.shoppingCart = shoppingCart;

            //load the products and do not await for it
            LoadProductsAsync();
        }

        private async Task LoadProductsAsync()
        {
            //Get the list of products from the API
            IEnumerable<NetworkResources.Product> products = await apiService.GetProductsAsync();

            //add the products to the catalog items
            foreach (var product in products)
            {
                CatalogItemViewModel catalogItem = new CatalogItemViewModel(product.Name, product.Price, null);

                catalogItem.AddToCartCommand = ReactiveCommand.Create(() =>
                {
                    shoppingCart.AddItem(product, catalogItem.Quantity);
                    UpdateShoppingCart?.Execute().Subscribe();
                });

                CatalogItems.Add(catalogItem);
            }
        }
    }
}
