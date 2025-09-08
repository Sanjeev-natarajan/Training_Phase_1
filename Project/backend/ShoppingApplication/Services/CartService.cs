using ShoppingApplication.Models.DTOs;
using ShoppingApplication.Repositories;

namespace ShoppingApplication.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<CartDto> GetCartAsync(int userId)
        {
            var cart = await _cartRepository.GetUserCartAsync(userId);

            return new CartDto
            {
                OrderId = cart.OrderId,
                UserId = cart.UserId,
                CartItems = cart.OrderItems.Select(i => new CartItemDto
                {
                    CartItemId = i.OrderItemId,
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    Price = i.Price,
                    Quantity = i.Quantity
                }).ToList()
            };
        }

        public async Task AddToCartAsync(int userId, AddCartItemDto dto)
        {
            await _cartRepository.AddItemAsync(userId, dto.ProductId, dto.Quantity);
        }

        public async Task UpdateCartItemAsync(int cartItemId, int quantity)
        {
            await _cartRepository.UpdateItemAsync(cartItemId, quantity);
        }

        public async Task RemoveCartItemAsync(int cartItemId)
        {
            await _cartRepository.RemoveItemAsync(cartItemId);
        }
    }
}
