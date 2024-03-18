using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetworkResources;

namespace StorefrontAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateOrder : ControllerBase
    {
        //PUT method to update an order
        [HttpPut]
        public IActionResult Put([FromBody] Order order)
        {
            //Get the existing order from the database
            Order? existingOrder = MemoryDatabase.Orders.Find(o => o.Id == order.Id);
            if (existingOrder == null) return NotFound();

            //verify that the total price is not negative and that the quantity of any item is not negative
            if(NegativeValuesExist(order)) return BadRequest("One or more values in the order were negative!");
            RemoveZeroQuantityItems(ref order);

            //Find the products in the order in the database, user cannot be trusted to send the correct product information
            List<Product> products = new List<Product>();
            foreach (var orderItem in order.OrderItems)
            {
                Product? product = MemoryDatabase.Products.Find(p => p.Id == orderItem.OrderId);
                if (product == null) return BadRequest("Product not found");
                products.Add(product);
            }

            //calculate the total price of the order, user cannot be trusted to send the correct total price
            decimal totalPrice = 0;
            foreach (var product in products)
            {
                OrderItem? orderItem = order.OrderItems.Find(oi => oi.OrderId == product.Id);
                if (orderItem == null) return BadRequest("Order item not found");
                totalPrice += product.Price * orderItem.Quantity;
            }
            //if the total price is zero, return a bad request stating "No items in order, did you mean to delete it?"
            if (totalPrice == 0) return BadRequest("No items in order, did you mean to delete it?");

            //if the total price doesn't match the total price in the order, return a 400
            if (totalPrice != order.TotalPrice) return BadRequest("Total price of cart on server does not match total price on client.");

            //copy the timestamp from the existing order to the new order
            order.Date = existingOrder.Date;

            //otherwise, update the order in the database
            MemoryDatabase.Orders.Remove(existingOrder);
            MemoryDatabase.Orders.Add(order);

            //update the order
            return Ok();
        }

        //private method to remove any items from the order that have a quantity of 0
        private void RemoveZeroQuantityItems(ref Order order)
        {
            order.OrderItems.RemoveAll(oi => oi.Quantity == 0);
        }

        //private method to verify price is not negative or quantity of any item is not negative
        private bool NegativeValuesExist(Order order)
        {
            if (order.TotalPrice < 0) return true;
            foreach (var orderItem in order.OrderItems)
            {
                if (orderItem.Price < 0 || orderItem.Quantity < 0) return true;
            }
            return false;
        }
    }
}
