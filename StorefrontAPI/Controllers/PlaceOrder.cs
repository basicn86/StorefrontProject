using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StorefrontAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceOrder : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] NetworkResources.OrderRequest orderRequest)
        {
            //create a new order
            var order = new NetworkResources.Order();
            decimal TotalPrice = 0;

            //for each order item in the request
            foreach (var orderedItem in orderRequest.OrderItems)
            {
                //get the matching product from the list of products
                NetworkResources.Product? product = MemoryDatabase.Products.Find(p => p.Id == orderedItem.Key);
                //if the product is not found, return an error
                if (product == null)
                {
                    return BadRequest("Product not found");
                }

                //create a new order item
                var newOrderItem = new NetworkResources.OrderItem
                {
                    ProductId = product.Id,
                    Quantity = orderedItem.Value,
                    Price = product.Price,
                    Name = product.Name
                };

                //add the order item to the order
                order.OrderItems.Add(newOrderItem);

                TotalPrice += newOrderItem.Price * newOrderItem.Quantity;
            }

            //make sure the total price matches the total price in the request
            if (TotalPrice != orderRequest.TotalPrice)
            {
                return BadRequest("Total price of cart on server does not match total price on client.");
            }

            //set the total price of the order
            order.TotalPrice = TotalPrice;

            //assign ID equal to the number of orders in the list
            order.Id = MemoryDatabase.Orders.Count;
            //add the order to the list of orders
            MemoryDatabase.Orders.Add(order);

            //return the order
            return Ok(order);
        }
    }
}
