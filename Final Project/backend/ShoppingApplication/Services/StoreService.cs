using ShoppingApplication.Models;
using ShoppingApplication.Models.DTOs;
using ShoppingApplication.Repositories;

namespace ShoppingApplication.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;

        public StoreService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<IEnumerable<Product>> GetMyProductsAsync(int storeId)
        {
            return await _storeRepository.GetProductsByStoreAsync(storeId);
        }

        public async Task<Product?> UpdateStockAsync(int storeId, int productId, int quantityChange)
        {
            return await _storeRepository.UpdateProductStockAsync(productId, storeId, quantityChange);
        }

        public async Task<Product?> ApplyPromotionAsync(int storeId, PromotionDto dto)
        {
            return await _storeRepository.ApplyPromotionAsync(dto.ProductId, storeId, dto.DiscountPercentage, dto.ValidUntil);
        }

        public async Task<StoreProfileDto?> GetStoreProfileAsync(int storeId)
        {
            var store = await _storeRepository.GetStoreByIdAsync(storeId);
            if (store == null) return null;

            return new StoreProfileDto
            {
                StoreId = store.UserId,
                StoreName = store.ShopName,
                Address = store.Address,
                PhoneNumber = store.PhoneNumber,
                LogoUrl = store.ShopName
            };
        }

        public async Task<StoreProfileDto?> UpdateStoreAsync(int storeId, UpdateStoreDto dto)
        {
            var store = await _storeRepository.UpdateStoreAsync(storeId, dto.Address, dto.PhoneNumber, dto.LogoUrl);
            if (store == null) return null;

            return new StoreProfileDto
            {
                StoreId = store.UserId,
                StoreName = store.ShopName,
                Address = store.Address,
                PhoneNumber = store.PhoneNumber,
                LogoUrl = store.ShopName
            };
        }
    }
}
