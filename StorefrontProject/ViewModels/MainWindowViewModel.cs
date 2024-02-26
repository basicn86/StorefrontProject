using ReactiveUI;
using StorefrontProject.Models;
using StorefrontProject.Models.Interfaces;
using System.Collections.Generic;
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
            //if debug mode is enabled, use the DebugShoppingCart
            ShoppingCartService.Instance = new ShoppingCart();
            ApiService.Instance = new ApiClient();

#if DEBUG
            shoppingCart = new DebugShoppingCart();
            MainContent = new CatalogViewModel(new DebugAPIService());
#else
            ShoppingCartService.Instance = new ShoppingCart();
            MainContent = new CatalogViewModel(ReactiveCommand.Create(() => { UpdateShoppingCartBtnText(); }));
#endif
            //Update the shopping cart button text
            UpdateShoppingCartBtnText();

            //When clicking Shop menu, open the CatalogViewModel
            ShopMenuCommand = ReactiveCommand.Create(() =>
            {
                //avoid reloading the catalog if it's already open
                if (MainContent is CatalogViewModel) return;
                MainContent = new CatalogViewModel(ReactiveCommand.Create(() => { UpdateShoppingCartBtnText(); }));
            });

            //When opening the shopping cart, open the ShoppingCartViewModel
            ShoppingCartBtnCommand = ReactiveCommand.Create(() =>
            {
                //avoid reloading the shopping cart if it's already open
                if (MainContent is ShoppingCartViewModel) return;
                MainContent = new ShoppingCartViewModel(ReactiveCommand.Create(() => { UpdateShoppingCartBtnText(); }));
            });

            //When clicking Orders menu, open the OrdersViewModel
            OrdersMenuCommand = ReactiveCommand.Create(() =>
            {
                //avoid reloading the orders if it's already open
                if (MainContent is OrdersViewModel) return;
                MainContent = new OrdersViewModel();
            });
        }

        //Update shopping cart button text
        public void UpdateShoppingCartBtnText()
        {
            //verify if the shopping cart service is null
            if (ShoppingCartService.Instance == null) return;

            //get the items in the shopping cart
            Dictionary<NetworkResources.Product, uint> items = ShoppingCartService.Instance.GetItems();

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
