using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingApplication.Models.DTOs;
using ShoppingApplication.Services;

namespace ShoppingApplication.Controllers
{
    [ApiController]
    [Route("api/cart")]
    [Authorize] 
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                throw new Exception("User not authenticated");

            return int.Parse(userIdClaim);
        }



        [HttpGet("mycart")]
        public async Task<IActionResult> GetCart()
        {
            var userId = GetCurrentUserId();
            var cart = await _cartService.GetCartAsync(userId);
            return Ok(cart);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddCartItemDto dto)
        {
            var userId = GetCurrentUserId();
            await _cartService.AddToCartAsync(userId, dto);
            return Ok("Item added to cart");
        }

        [HttpPut("update/{cartItemId}")]
        public async Task<IActionResult> UpdateCartItem(int cartItemId, [FromBody] AddCartItemDto dto)
        {
            var userId = GetCurrentUserId();
            await _cartService.UpdateCartItemAsync(cartItemId, dto.Quantity);
            return Ok("Cart item updated");
        }

        [HttpDelete("remove/{cartItemId}")]
        public async Task<IActionResult> RemoveCartItem(int cartItemId)
        {
            var userId = GetCurrentUserId();
            await _cartService.RemoveCartItemAsync(cartItemId);
            return Ok("Cart item removed");
        }
    }
}
