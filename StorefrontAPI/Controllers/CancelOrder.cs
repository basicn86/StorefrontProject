using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StorefrontAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CancelOrder : ControllerBase
    {
        [HttpDelete]
        public IActionResult DeleteOrder(int orderId)
        {
            //delete the order from the database
            int Removed = MemoryDatabase.Orders.RemoveAll(o => o.Id == orderId);

            //if the order was removed, return 200 OK
            if (Removed > 0)
            {
                return Ok();
            }

            //if the order was not removed, return 404 Not Found
            return NotFound();
        }
    }
}
