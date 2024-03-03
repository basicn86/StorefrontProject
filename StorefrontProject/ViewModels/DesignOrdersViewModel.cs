using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkResources;

namespace StorefrontProject.ViewModels
{
    public class DesignOrdersViewModel : OrdersViewModel
    {
        //constructor with example data
        public DesignOrdersViewModel()
        {
            OrderList = new System.Collections.ObjectModel.ObservableCollection<OrderViewModel>
            {
                new OrderViewModel
                {
                    Id = 1,
                    TotalPrice = 100,
                    Date = DateTime.Now,
                    OrderItems = new System.Collections.ObjectModel.ObservableCollection<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductId = 1,
                            Quantity = 2,
                            Price = 60m,
                            Name = "Product 1"
                        },
                        new OrderItem
                        {
                            ProductId = 2,
                            Quantity = 3,
                            Price = 20m,
                            Name = "Product 2"
                        }
                    }
                },
                new OrderViewModel
                {
                    Id = 2,
                    TotalPrice = 320,
                    Date = DateTime.Now.AddDays(-1),
                    OrderItems = new System.Collections.ObjectModel.ObservableCollection<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductId = 3,
                            Quantity = 4,
                            Price = 30m,
                            Name = "Product 3"
                        },
                        new OrderItem
                        {
                            ProductId = 4,
                            Quantity = 5,
                            Price = 40m,
                            Name = "Product 4"
                        }
                    }
                }
            };
        }
    }
}
