using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using StorefrontProject.Models;
using StorefrontProject.Models.Interfaces;

namespace StorefrontProject.ViewModels
{
    public class CatalogViewModel : ViewModelBase
    {
        public ObservableCollection<CatalogItemViewModel> CatalogItems { get; set; }

        private IAPIService apiService;
        public CatalogViewModel(IAPIService _apiService)
        {
            CatalogItems = new ObservableCollection<CatalogItemViewModel>();
            apiService = _apiService;

            //load the products and do not await for it
            LoadProductsAsync();
        }

        private async Task LoadProductsAsync()
        {
            //list of products
            IEnumerable<NetworkResources.Product> products = await apiService.GetProductsAsync();

            foreach (var product in products)
            {
                CatalogItems.Add(new CatalogItemViewModel(product.Name, product.Price, null));
            }
        }
    }
}
