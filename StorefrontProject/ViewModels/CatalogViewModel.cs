using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using StorefrontProject.Models;

namespace StorefrontProject.ViewModels
{
    public class CatalogViewModel : ViewModelBase
    {
        public ObservableCollection<CatalogItemViewModel> CatalogItems { get; set; }

        private StoreDbContext context = new StoreDbContext();
        public CatalogViewModel()
        {
            CatalogItems = new ObservableCollection<CatalogItemViewModel>();
            context.ResetDatabase();

            foreach (var product in context.Products)
            {
                if (product.ProductImage == null)
                {
                    continue;
                }

                using (MemoryStream ms = new MemoryStream(product.ProductImage))
                {
                    CatalogItemViewModel item = new CatalogItemViewModel(product.Name, product.Price, new Bitmap(ms));
                    CatalogItems.Add(item);
                }
            }
        }
    }
}
