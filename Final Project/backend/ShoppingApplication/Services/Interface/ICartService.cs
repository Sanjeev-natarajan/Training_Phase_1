using ShoppingApplication.Models.DTOs;
using System.Threading.Tasks;

namespace ShoppingApplication.Services
{
    public interface ICartService
    {
        Task<CartDto> GetCartAsync(int userId);
        Task AddToCartAsync(int userId, AddCartItemDto dto);
        Task UpdateCartItemAsync(int cartItemId, int quantity);
        Task RemoveCartItemAsync(int cartItemId);
    }
}
