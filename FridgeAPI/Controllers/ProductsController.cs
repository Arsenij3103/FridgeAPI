using Fridge.API.Services;
using Fridge.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fridge.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public ActionResult<List<Product>> GetAllProducts()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            _productService.CreateProduct(product);
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            if(id!=product.Id)
            {
                return BadRequest();
            }
             if (!_productService.ProductExists(id))
            {
                return NotFound();
            }
            _productService.UpdateProduct(product);
             return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            if(!_productService.ProductExists(id))
            {
                return NotFound();
            }
            _productService.DeleteProduct(id);
            return Ok();
        }
    }
}
