using ReactiveUI;
using StorefrontProject.Models;
using StorefrontProject.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace StorefrontProject.ViewModels
{
    public class ShoppingCartViewModel : ViewModelBase
    {
        //observable collection of ShoppingCartItemViewModel
        public ObservableCollection<ShoppingCartItemViewModel> ShoppingCartItems { get; set; }

        private ReactiveCommand<Unit, Unit> UpdateCartCommand { get; }

        private IShoppingCart shoppingCart { get; }
        public ShoppingCartViewModel(IShoppingCart _shoppingCart, ReactiveCommand<Unit, Unit> updateCartCommand)
        {
            ShoppingCartItems = new ObservableCollection<ShoppingCartItemViewModel>();

            //set the shopping cart
            shoppingCart = _shoppingCart;
            //set the update cart command
            UpdateCartCommand = updateCartCommand;

            //get items from the shopping cart
            var items = shoppingCart.GetItems();

            //for each item in the shopping cart, add a new ShoppingCartItemViewModel to the ShoppingCartItems collection
            foreach (var item in items)
            {
                ShoppingCartItemViewModel i = new ShoppingCartItemViewModel
                {
                    Name = item.Key.Name,
                    Price = item.Key.Price,
                    Quantity = item.Value
                };

                i.RemoveItemCommand = ReactiveCommand.Create(() =>
                {
                    shoppingCart.RemoveItem(item.Key);
                    ShoppingCartItems.Remove(i);
                    UpdateCartCommand.Execute().Subscribe();
                });

                i.UpdateCartCommand = ReactiveCommand.Create(() =>
                {
                    shoppingCart.UpdateQuantity(item.Key, (uint)i.Quantity);
                    UpdateCartCommand.Execute().Subscribe();
                });

                //add the item to the collection
                ShoppingCartItems.Add(i);
            }
        }
    }
}
