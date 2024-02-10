using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;

namespace StorefrontProject.ViewModels
{
    public class CatalogViewModel : ViewModelBase
    {
        public ObservableCollection<Bitmap> TestProducts { get; set; } = new ObservableCollection<Bitmap> { new Bitmap("C:\\Users\\Nedim\\source\\repos\\StorefrontProject\\StorefrontProject\\Assets\\apple.jpg"),
        new Bitmap("C:\\Users\\Nedim\\source\\repos\\StorefrontProject\\StorefrontProject\\Assets\\banana.jpg"),
        new Bitmap("C:\\Users\\Nedim\\source\\repos\\StorefrontProject\\StorefrontProject\\Assets\\carrot.jpg"),
        new Bitmap("C:\\Users\\Nedim\\source\\repos\\StorefrontProject\\StorefrontProject\\Assets\\durian.webp"),
        new Bitmap("C:\\Users\\Nedim\\source\\repos\\StorefrontProject\\StorefrontProject\\Assets\\eggplant.webp"),
        new Bitmap("C:\\Users\\Nedim\\source\\repos\\StorefrontProject\\StorefrontProject\\Assets\\figs.jpg")};
    }
}
