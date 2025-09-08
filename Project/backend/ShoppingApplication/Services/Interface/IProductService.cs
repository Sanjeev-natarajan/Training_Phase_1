using ShoppingApplication.Models.DTOs;

namespace ShoppingApplication.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(int id);
        Task<ProductDto?> GetProductByName(string name);
        Task<ProductDto> CreateProductAsync(CreateProductDto dto, string uploadPath, int createdByUserId);

        Task<ProductDto?> UpdateProductAsync(int id, CreateProductDto dto, string uploadPath);
        Task<bool> DeleteProductAsync(int id);

        Task<IEnumerable<ProductDto>> SearchProductsAsync(string? query, string? category, decimal? priceMin, decimal? priceMax);

    }
}
