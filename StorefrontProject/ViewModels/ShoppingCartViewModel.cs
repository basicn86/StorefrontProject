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

        private ReactiveCommand<Unit, Unit>? UpdateCartCommand { get; }

        //Checkout button command
        public ReactiveCommand<Unit, Unit>? CheckoutCommand { get; }
        //Clear cart button command
        public ReactiveCommand<Unit, Unit>? ClearCartCommand { get; }

        public ShoppingCartViewModel(ReactiveCommand<Unit, Unit> updateCartCommand)
        {
            if (ShoppingCartService.Instance == null) return;

            ShoppingCartItems = new ObservableCollection<ShoppingCartItemViewModel>();

            //set the update cart command
            UpdateCartCommand = updateCartCommand;

            //get items from the shopping cart
            var items = ShoppingCartService.Instance?.GetItems();

            //if the items are null, or empty, return
            if (items == null || items.Count == 0)
            {
                return;
            }

            //create the CheckoutCommand
            CheckoutCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                await ShoppingCartService.Instance?.Checkout();
                UpdateCartCommand.Execute().Subscribe();
                //clear the ShoppingCartItems collection
                ShoppingCartItems.Clear();
            });

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
                    ShoppingCartService.Instance?.RemoveItem(item.Key);
                    ShoppingCartItems.Remove(i);
                    UpdateCartCommand.Execute().Subscribe();
                });

                i.UpdateCartCommand = ReactiveCommand.Create(() =>
                {
                    ShoppingCartService.Instance?.UpdateQuantity(item.Key, (uint)i.Quantity);
                    UpdateCartCommand.Execute().Subscribe();
                });

                //add the item to the collection
                ShoppingCartItems.Add(i);
            }
        }
    }
}
