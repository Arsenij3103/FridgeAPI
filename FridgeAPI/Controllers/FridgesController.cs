using Fridge.API.Services;
using Fridge.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fridge.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FridgesController : ControllerBase
    {
        private readonly IFridgeService _fridgeService;
        public FridgesController(IFridgeService fridgeService)
        {
            _fridgeService = fridgeService;
        }
        [HttpGet]
        public ActionResult<List<FridgeEntity>> GetAllFridges()
        {
            var fridges = _fridgeService.GetAllFridges();
            return Ok(fridges);
        }
        [HttpGet("{id}")]
        public ActionResult<FridgeEntity> GetFridgeById(int id)
        {
            var fridge = _fridgeService.GetFridgeById(id);
            if(fridge==null)
            {
                return NotFound();
            }
            return Ok(fridge);
        }
        [HttpPost]
        public IActionResult CreateFridge(string name)
        {
            _fridgeService.CreateFridge(name);
            return Ok();
        }
    }
}
