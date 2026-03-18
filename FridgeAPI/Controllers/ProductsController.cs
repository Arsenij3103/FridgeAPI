using Fridge.Aplication.Interfaces.Services;
using Fridge.Application.Exceptions;
using Fridge.Contracts.Products;
using Fridge.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<ProductResponse>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            var response = products.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                DefaultQuantity = p.DefaultQuantity
            }).ToList();
            return Ok(response);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var response = new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                DefaultQuantity = product.DefaultQuantity
            };
            return Ok(response);
        }
        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            try
            {
                var product = new Product
                {
                    Name = request.Name,
                    DefaultQuantity = request.DefaultQuantity,
                };
                await _productService.CreateProductAsync(product);
                return Ok();
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize(Roles = "Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductRequest request)
        {
            try
            {
                var existingProduct = await _productService.GetProductByIdAsync(id);
                if (existingProduct == null)
                {
                    return NotFound();
                }
                existingProduct.Name = request.Name;
                existingProduct.DefaultQuantity = request.DefaultQuantity;
                await _productService.UpdateProductAsync(existingProduct);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
