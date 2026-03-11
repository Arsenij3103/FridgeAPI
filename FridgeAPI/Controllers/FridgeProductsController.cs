using Fridge.API.Services;
using Fridge.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fridge.API.Controllers
{
    [ApiController]
    [Route("api/fridges/{fridgeid}/products")]
    public class FridgeProductsController:ControllerBase
    {
        private readonly IFridgeProductService _fridgeProductService;
        public FridgeProductsController(IFridgeProductService fridgeProductService)
        {
            _fridgeProductService = fridgeProductService;
        }
        [HttpGet] 
        public ActionResult <List<FridgeProduct>> GetProductsInFridge(int fridgeid)
        {
            var products = _fridgeProductService.GetProductInFridge(fridgeid);
            return Ok(products);
        }
        [HttpPost("{productid}")]
        public IActionResult AddProductToFridge(int fridgeid, int productid)
        {
            _fridgeProductService.AddProductToFridge(fridgeid, productid);
            return Ok();
        }
        [HttpPut("{productid}")]
        public IActionResult UpdateProductQuantity(int fridgeid,int productid, [FromQuery] int quantity)
        {
            _fridgeProductService.UpdateProductQuantity(fridgeid,productid,quantity);
            return Ok();
        }
        [HttpDelete("{productid}")]
        public IActionResult RemoveProductFromFridge(int productid,int fridgeid)
        {
            _fridgeProductService.RemoveProductFromFridge(fridgeid, productid);
            return Ok();
        }
    }
}
