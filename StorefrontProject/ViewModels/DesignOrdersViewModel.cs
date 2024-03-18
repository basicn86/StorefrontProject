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
            OrderList = new System.Collections.ObjectModel.ObservableCollection<OrderViewModel>();

            Order testOrder = new Order()
            {
                Id = 1,
                Date = DateTime.Now,
                TotalPrice = 30.00m,
                OrderItems = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        OrderId = 1,
                        Name = "Product 1",
                        Quantity = 1,
                        Price = 10.00m
                    },
                    new OrderItem()
                    {
                        OrderId = 2,
                        Name = "Product 2",
                        Quantity = 2,
                        Price = 20.00m
                    }
                }
            };
            OrderList.Add(new OrderViewModel(testOrder));

            //add one more test order
            testOrder = new Order()
            {
                Id = 2,
                Date = DateTime.Now,
                TotalPrice = 100.00m,
                OrderItems = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        OrderId = 2,
                        Name = "Product 2",
                        Quantity = 2,
                        Price = 20.00m
                    },
                    new OrderItem()
                    {
                        OrderId = 3,
                        Name = "Product 3",
                        Quantity = 2,
                        Price = 30.00m
                    }
                }
            };

            OrderList.Add(new OrderViewModel(testOrder));
        }
    }
}
