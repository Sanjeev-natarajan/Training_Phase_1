using ShoppingApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingApplication.Repositories
{
    public interface ICartRepository
    {
        Task<Order> GetUserCartAsync(int userId);
        Task AddItemAsync(int userId, int productId, int quantity);
        Task UpdateItemAsync(int cartItemId, int quantity);
        Task RemoveItemAsync(int cartItemId);

    }
}
