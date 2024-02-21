using ReactiveUI;
using StorefrontProject.Models;
using StorefrontProject.Models.Interfaces;
using System.Reactive;

namespace StorefrontProject.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase? mainContent;
        public ViewModelBase? MainContent {
            get => mainContent;
            set => this.RaiseAndSetIfChanged(ref mainContent, value);
        }

        private string shoppingCartBtnText = "Shopping Cart";
        //shopping cart btn text
        public string ShoppingCartBtnText
        {
            get => shoppingCartBtnText;
            set => this.RaiseAndSetIfChanged(ref shoppingCartBtnText, value);
        }

        //Shopping Cart button command
        public ReactiveCommand<Unit, Unit> ShoppingCartBtnCommand { get; set; }
        //Orders menu
        public ReactiveCommand<Unit, Unit> OrdersMenuCommand { get; set; }
        //Shop Menu Command
        public ReactiveCommand<Unit, Unit> ShopMenuCommand { get; set; }


        public MainWindowViewModel()
        {
            //When clicking Shop menu, open the CatalogViewModel
            ShopMenuCommand = ReactiveCommand.Create(() =>
            {
                //if debug mode is enabled, use the DebugAPIService
#if DEBUG
                MainContent = new CatalogViewModel(new DebugAPIService());
#else
                MainContent = new CatalogViewModel(new APIService());
#endif
            });

            //When opening the shopping cart, open the ShoppingCartViewModel
            ShoppingCartBtnCommand = ReactiveCommand.Create(() =>
            {
                //ifif debug mode is enabled, use the DebugShoppingCart
                #if DEBUG
                MainContent = new ShoppingCartViewModel(new DebugShoppingCart(), ReactiveCommand.Create(() => { }));
                #else
                MainContent = new ShoppingCartViewModel(new ShoppingCart(), ReactiveCommand.Create(() => { }));
                #endif
            });
        }
    }
}
