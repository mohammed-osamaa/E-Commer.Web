using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared;
using Shared.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // BaseURL/api/products
    public class ProductsController(IServiceManager _serviceManger) : ControllerBase
    {
        // Get all products
        [HttpGet] //GET : BaseURL/api/products
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts([FromQuery]ProductQueryParameters queryParameters)
        {
            var products = await _serviceManger.ProductServices.GetProductsAsync(queryParameters);
            return Ok(products);
        }
        // Get product by id    
        [HttpGet("{id:int}")] //GET : BaseURL/api/products/1
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _serviceManger.ProductServices.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }
        // Get all Brands
        [HttpGet("brands")] //GET : BaseURL/api/products/brands
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var brands = await _serviceManger.ProductServices.GetBrandsAsync();
            return Ok(brands);
        }
        // Get all Types
        [HttpGet("types")] //GET : BaseURL/api/products/types
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes()
        {
            var types = await _serviceManger.ProductServices.GetTypesAsync();
            return Ok(types);
        }
    }
}
