using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StorefrontAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetProducts : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            //return the list of products from the memory database
            return Ok(MemoryDatabase.Products);
        }
    }
}
