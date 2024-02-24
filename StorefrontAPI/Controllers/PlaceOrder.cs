using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StorefrontAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceOrder : ControllerBase
    {
        //store orders in memory for now
        public static List<NetworkResources.Order> _orders = new List<NetworkResources.Order>();

        [HttpPost]
        public IActionResult Post([FromBody] NetworkResources.OrderRequest orderRequest)
        {
            //create a new order
            var order = new NetworkResources.Order();

            //for each order item in the request
            foreach (var orderItem in orderRequest.OrderItems)
            {
                //create a new order item
                var newOrderItem = new NetworkResources.OrderItem
                {
                    //set the product ID
                    ProductId = orderItem.Key,
                    //set the quantity
                    Quantity = orderItem.Value
                };

                //add the order item to the order
                order.OrderItems.Add(newOrderItem);
            }

            //set the total price
            //TODO: Compare the total price from the client to the total price calculated on the server
            //TODO: if not the same, return an error
            order.TotalPrice = orderRequest.TotalPrice;

            //add the order to the list of orders
            _orders.Add(order);

            //return the order
            return Ok(order);
        }
    }
}
