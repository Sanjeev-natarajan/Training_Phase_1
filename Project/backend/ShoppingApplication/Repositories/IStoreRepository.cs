using ShoppingApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingApplication.Repositories
{
    public interface IStoreRepository
    {
        Task<IEnumerable<Product>> GetProductsByStoreAsync(int storeId);
        Task<Product?> GetProductByIdAsync(int productId, int storeId);
        Task<Product?> UpdateProductStockAsync(int productId, int storeId, int quantityChange);
        Task<Product?> ApplyPromotionAsync(int productId, int storeId, decimal discountPercentage, DateTime validUntil);
        Task<User?> GetStoreByIdAsync(int storeId);
        Task<User?> UpdateStoreAsync(int storeId, string address, string phone, string? logoUrl);
    }
}
