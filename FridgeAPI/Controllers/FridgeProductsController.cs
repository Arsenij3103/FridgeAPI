using Fridge.Aplication.Interfaces.Services;
using Fridge.Application.Exceptions;
using Fridge.Contracts.FridgeProducts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fridge.API.Controllers
{
    [ApiController]
    [Route("api/fridges/{fridgeid}/products")]
    public class FridgeProductsController : ControllerBase
    {
        private readonly IFridgeProductService _fridgeProductService;
        public FridgeProductsController(IFridgeProductService fridgeProductService)
        {
            _fridgeProductService = fridgeProductService;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<FridgeProductResponse>>> GetProductsByFridgeId(int fridgeid)
        {
            try
            {
                var fridgeProducts = await _fridgeProductService.GetProductsByFridgeIdAsync(fridgeid);
                var response = fridgeProducts.Select(fp => new FridgeProductResponse
                {
                    FridgeId = fp.FridgeId,
                    ProductId = fp.ProductId,
                    Quantity = fp.Quantity
                });
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [Authorize(Roles = "Manager")]
        [HttpPost("{productid}")]
        public async Task<IActionResult> AddProductToFridge(int fridgeid, int productid)
        {
            try
            {
                await _fridgeProductService.AddProductToFridgeAsync(fridgeid, productid);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [Authorize(Roles = "Manager")]
        [HttpPut("{productid}")]
        public async Task<IActionResult> UpdateProductQuantity(int fridgeid, int productid, [FromBody] UpdateFridgeProductQuantityRequest request)
        {
            try
            {
                await _fridgeProductService.UpdateProductQuantityAsync(fridgeid, productid, request.Quantity);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize(Roles = "Manager")]
        [HttpDelete("{productid}")]
        public async Task<IActionResult> RemoveProductFromFridge(int productid, int fridgeid)
        {
            try
            {
                await _fridgeProductService.RemoveProductFromFridgeAsync(fridgeid, productid);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
