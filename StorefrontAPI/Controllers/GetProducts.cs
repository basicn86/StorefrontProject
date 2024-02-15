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
            List <Product> products = new List<Product>();

            //add three fake products
            products.Add(new Product { Id = 1, Name = "Product 1", Price = 10.00m });
            products.Add(new Product { Id = 2, Name = "Product 2", Price = 20.00m });
            products.Add(new Product { Id = 3, Name = "Product 3", Price = 30.00m });

            //return the list of products in a JSON response
            return Ok(products);
        }
    }
}
