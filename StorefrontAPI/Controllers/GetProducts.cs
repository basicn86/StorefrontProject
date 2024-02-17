using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorefrontAPI.Models;

namespace StorefrontAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetProducts : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            //list of products
            List <NetworkResources.Product> products = new List<NetworkResources.Product>();

            //add three fake products
            products.Add(new NetworkResources.Product { Id = 1, Name = "Walnuts", Price = 50.00m });
            products.Add(new NetworkResources.Product { Id = 2, Name = "Pineapples", Price = 20.00m });
            products.Add(new NetworkResources.Product { Id = 3, Name = "Coconuts", Price = 30.00m });

            //return the list of products in a JSON response
            return Ok(products);
        }
    }
}
