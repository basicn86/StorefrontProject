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

        private StoreDbContext context;
        public CatalogViewModel()
        {
            context = new StoreDbContext("store.db");
            CatalogItems = new ObservableCollection<CatalogItemViewModel>();

#if DEBUG
            context.ResetDatabase(); //reset database in debug config
#endif

            //This loop uses LINQ to get the first 10 products from the database and adds them to the CatalogItems collection.
            foreach (var product in (from p in context.Products select p).Take(10))
            {
                //if the image fails to load, skip it
                if (product.ProductImage == null)
                {
                    continue;
                }

                //Converts the byte array to a Bitmap and adds it to the CatalogItems collection.
                using (MemoryStream ms = new MemoryStream(product.ProductImage))
                {
                    CatalogItemViewModel item = new CatalogItemViewModel(product.Name, product.Price, new Bitmap(ms));
                    CatalogItems.Add(item);
                }
            }
        }
    }
}
