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

        IShoppingCart shoppingCart;

        public MainWindowViewModel()
        {
            //if debug mode is enabled, use the DebugShoppingCart
            ShoppingCartService.Instance = new ShoppingCart();

#if DEBUG
            shoppingCart = new DebugShoppingCart();
            MainContent = new CatalogViewModel(new DebugAPIService());
#else
            shoppingCart = new ShoppingCart();
            MainContent = new CatalogViewModel(new APIService());
#endif
            //Update the shopping cart button text
            UpdateShoppingCartBtnText();

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
                MainContent = new ShoppingCartViewModel(ReactiveCommand.Create(() => { UpdateShoppingCartBtnText(); }));
            });
        }

        //Update shopping cart button text
        public void UpdateShoppingCartBtnText()
        {
            //get the items in the shopping cart
            var items = shoppingCart.GetItems();

            uint TotalItems = 0;
            decimal TotalPrice = 0;

            //for each item in the shopping cart, add the quantity to the total items
            foreach (var item in items)
            {
                TotalPrice += item.Key.Price * item.Value;
                TotalItems += item.Value;
            }

            //set the shopping cart button text to "Shopping Cart $price (TotalItems)"
            ShoppingCartBtnText = $"Shopping Cart {TotalPrice:C} ({TotalItems})";
        }
    }
}
