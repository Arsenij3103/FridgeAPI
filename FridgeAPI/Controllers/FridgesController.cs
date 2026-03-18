using Fridge.Aplication.Interfaces.Services;
using Fridge.Contracts.Fridges;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<FridgeResponse>>> GetAllFridges()
        {
            var fridges = await _fridgeService.GetAllFridgesAsync();
            var response = fridges.Select(f => new FridgeResponse
            {
                Id = f.Id,
                Name = f.Name
            }).ToList();

            return Ok(response);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<FridgeResponse>> GetFridgeById(int id)
        {
            var fridge = await _fridgeService.GetFridgeByIdAsync(id);
            if (fridge == null)
            {
                return NotFound();
            }
            var response = new FridgeResponse
            {
                Id = fridge.Id,
                Name = fridge.Name
            };
            return Ok(response);
        }
        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateFridge([FromBody] CreateFridgeRequest request)
        {
            try
            {
                await _fridgeService.CreateFridgeAsync(request.Name);
                return Ok();
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
