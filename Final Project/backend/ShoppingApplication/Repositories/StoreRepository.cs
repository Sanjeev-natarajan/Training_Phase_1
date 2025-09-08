using Microsoft.EntityFrameworkCore;
using ShoppingApplication.Data;
using ShoppingApplication.Models;

namespace ShoppingApplication.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly AppDbContext _context;

        public StoreRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsByStoreAsync(int storeId)
        {
            return await _context.Products
                .Where(p => p.CreatedByStoreId == storeId) 
                .ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int productId, int storeId)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == productId && p.CreatedByStoreId == storeId);
        }

        public async Task<Product?> UpdateProductStockAsync(int productId, int storeId, int quantityChange)
        {
            var product = await GetProductByIdAsync(productId, storeId);
            if (product == null) return null;

            product.Stock += quantityChange;
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> ApplyPromotionAsync(int productId, int storeId, decimal discountPercentage, DateTime validUntil)
        {
            var product = await GetProductByIdAsync(productId, storeId);
            if (product == null) return null;

            product.Price = product.Price * (1 - discountPercentage / 100);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<User?> GetStoreByIdAsync(int storeId)
        {
            return await _context.Users.FindAsync(storeId);
        }

        public async Task<User?> UpdateStoreAsync(int storeId, string address, string phone, string? logoUrl)
        {
            var store = await GetStoreByIdAsync(storeId);
            if (store == null) return null;

            store.Address = address;
            store.PhoneNumber = phone;
            store.ShopName = logoUrl; 
            await _context.SaveChangesAsync();
            return store;
        }
    }
}
