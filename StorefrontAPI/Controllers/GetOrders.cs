using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StorefrontAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetOrders : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            //return the list of orders
            return Ok(PlaceOrder._orders);
        }
    }
}
