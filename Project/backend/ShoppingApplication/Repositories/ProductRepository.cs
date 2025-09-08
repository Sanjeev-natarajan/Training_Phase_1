using Microsoft.EntityFrameworkCore;
using ShoppingApplication.Data;
using ShoppingApplication.Models;

namespace ShoppingApplication.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product?> GetByNameAsync(string name)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
        }


        public async Task<Product> AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
        
         public async Task<IEnumerable<Product>> SearchProductsAsync(string? query, string? category, decimal? priceMin, decimal? priceMax)
        {
            var products = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                products = products.Where(p => p.Name.Contains(query));
            }

            if (!string.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.Category == category);
            }

            if (priceMin.HasValue)
            {
                products = products.Where(p => p.Price >= priceMin.Value);
            }

            if (priceMax.HasValue)
            {
                products = products.Where(p => p.Price <= priceMax.Value);
            }

            return await products.ToListAsync();
        }
    }
}
