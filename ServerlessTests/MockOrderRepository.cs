using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using ServerlessAPI.Entities;
using ServerlessAPI.Repositories;

namespace ServerlessTests
{
    class MockOrderRepository : IOrderRepository
    {
        public List<Order> orders = new List<Order>();
        public Task AddOrderAsync(Order order)
        {
            orders.Add(order);
            return Task.CompletedTask;
        }

        public Task DeleteOrderAsync(Guid id)
        {
            orders.RemoveAll(order => order.Id == id);
            return Task.CompletedTask;
        }

        public Task<IList<Order>> GetOrdersAsync(int limit = 10)
        {
            return Task.FromResult<IList<Order>>(orders.Take(limit).ToList());
        }

        public Task UpdateOrderAsync(Order order)
        {
            orders.RemoveAll(o => o.Id == order.Id);
            orders.Add(order);
            return Task.CompletedTask;
        }
    }
}
