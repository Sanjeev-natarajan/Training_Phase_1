using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingApplication.Models.DTOs;
using ShoppingApplication.Services;

namespace ShoppingApplication.Controllers
{
    [ApiController]
    [Route("api/store")]
    [Authorize] 
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        private int GetCurrentStoreId()
        {
            var storeIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(storeIdClaim))
                throw new Exception("Store not authenticated");

            return int.Parse(storeIdClaim);
        }

        [HttpGet("myproducts")]
        public async Task<IActionResult> GetMyProducts()
        {
            var storeId = GetCurrentStoreId();
            var products = await _storeService.GetMyProductsAsync(storeId);
            return Ok(products);
        }

        [HttpPut("products/{id}/stock")]
        public async Task<IActionResult> UpdateStock(int id, [FromBody] UpdateStockDto dto)
        {
            var storeId = GetCurrentStoreId();
            var updated = await _storeService.UpdateStockAsync(storeId, id, dto.QuantityChange);
            if (updated == null) return NotFound("Product not found or does not belong to store");
            return Ok(updated);
        }

        [HttpPost("products/promotion")]
        public async Task<IActionResult> ApplyPromotion([FromBody] PromotionDto dto)
        {
            var storeId = GetCurrentStoreId();
            var updated = await _storeService.ApplyPromotionAsync(storeId, dto);
            if (updated == null) return NotFound("Product not found or does not belong to store");
            return Ok(updated);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetProfile()
        {
            var storeId = GetCurrentStoreId();
            var profile = await _storeService.GetStoreProfileAsync(storeId);
            if (profile == null) return NotFound("Store not found");
            return Ok(profile);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateStoreDto dto)
        {
            var storeId = GetCurrentStoreId();
            var updated = await _storeService.UpdateStoreAsync(storeId, dto);
            if (updated == null) return NotFound("Store not found");
            return Ok(updated);
        }
    }
}
