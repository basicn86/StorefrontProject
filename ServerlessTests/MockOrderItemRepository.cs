using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerlessAPI.Entities;
using ServerlessAPI.Repositories;

namespace ServerlessTests
{
    class MockOrderItemRepository : IOrderItemRepository
    {
        public List<OrderItem> orderItems = new List<OrderItem>();
        public Task AddOrderItemsAsync(IList<OrderItem> orderItem)
        {
            orderItems.AddRange(orderItem);
            return Task.CompletedTask;
        }

        //Note: This deletes all the order items by the order id, not by their actual id
        public Task DeleteOrderItemsAsync(Guid id)
        {
            orderItems.RemoveAll(orderItem => orderItem.OrderId == id);
            return Task.CompletedTask;
        }

        public Task<IList<OrderItem>> GetOrderItemsAsync(Guid id)
        {
            return Task.FromResult<IList<OrderItem>>(orderItems.Where(orderItem => orderItem.OrderId == id).ToList());
        }

        public Task UpdateOrderItemsAsync(IList<OrderItem> orderItems)
        {
            this.orderItems.RemoveAll(o => o.OrderId == orderItems[0].OrderId);
            this.orderItems.AddRange(orderItems);
            return Task.CompletedTask;
        }
    }
}
