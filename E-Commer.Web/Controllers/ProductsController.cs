using E_Commer.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commer.Web.Controllers
{
    [Route("api/[controller]")] //BaseURL/api/products
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet] //Get : BaseURL/api/products
        public ActionResult<Product> GetProducts()
        {
            return new Product { Id = 1000 };
        }
        [HttpGet("{id}")] //Get : BaseURL/api/products/id
        public ActionResult<Product> GetProduct(int id)
        {
            return new Product { Id = id };
        }
        [HttpPost] //Post : BaseURL/api/products
        public ActionResult<Product> CreateProduct([FromBody] Product product)
        {
            return new Product { Id = 1 };
        }
        [HttpPut] //Put : BaseURL/api/products?id
        public ActionResult<Product> UpdateProduct(int id, [FromBody] Product product)
        {
            return new Product { Id = id +10 };
        }
        [HttpDelete] //Delete : BaseURL/api/products?id
        public ActionResult DeleteProduct(int id)
        {
            return NoContent();
        }
    }
}
