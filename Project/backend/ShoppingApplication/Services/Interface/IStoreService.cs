using ShoppingApplication.Models.DTOs;
using ShoppingApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingApplication.Services
{
    public interface IStoreService
    {
        Task<IEnumerable<Product>> GetMyProductsAsync(int storeId);
        Task<Product?> UpdateStockAsync(int storeId, int productId, int quantityChange);
        Task<Product?> ApplyPromotionAsync(int storeId, PromotionDto dto);
        Task<StoreProfileDto?> GetStoreProfileAsync(int storeId);
        Task<StoreProfileDto?> UpdateStoreAsync(int storeId, UpdateStoreDto dto);
    }
}
