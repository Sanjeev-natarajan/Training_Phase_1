using ShoppingApplication.Models;
using ShoppingApplication.Models.DTOs;
using ShoppingApplication.Repositories;

namespace ShoppingApplication.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.Select(p => MapToDto(p));
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            return product == null ? null : MapToDto(product);
        }

        public async Task<ProductDto?> GetProductByName(string name)
        {
            var product = await _repository.GetByNameAsync(name);
            return product == null ? null : MapToDto(product);
        }
        public async Task<ProductDto> CreateProductAsync(CreateProductDto dto, string uploadPath,int createdByUserId)
        {
            string? imageUrl = await SaveImageAsync(dto.Image, uploadPath);

            var product = new Product
            {
                Name = dto.Name,
                Brand = dto.Brand,
                Price = dto.Price,
                Category = dto.Category,
                Stock = dto.Stock,
                ImageUrl = imageUrl,
                CreatedByStoreId = createdByUserId
            };

            var savedProduct = await _repository.AddAsync(product);
            return MapToDto(savedProduct);
        }

        public async Task<ProductDto?> UpdateProductAsync(int id, CreateProductDto dto, string uploadPath)
        {
            var existingProduct = await _repository.GetByIdAsync(id);
            if (existingProduct == null) return null;


            if (!string.IsNullOrWhiteSpace(dto.Name))
                existingProduct.Name = dto.Name;

            if (!string.IsNullOrWhiteSpace(dto.Brand))
                existingProduct.Brand = dto.Brand;

            if (dto.Price > 0)
                existingProduct.Price = dto.Price;

            if (!string.IsNullOrWhiteSpace(dto.Category))
                existingProduct.Category = dto.Category;

            if (dto.Stock > 0)
                existingProduct.Stock = dto.Stock;

            if (dto.Image != null)
                existingProduct.ImageUrl = await SaveImageAsync(dto.Image, uploadPath);

            var updatedProduct = await _repository.UpdateAsync(existingProduct);
            return MapToDto(updatedProduct);
        }


        public async Task<bool> DeleteProductAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        private async Task<string?> SaveImageAsync(IFormFile? image, string uploadPath)
        {
            if (image == null || image.Length == 0) return null;

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return "/uploads/" + fileName;
        }

        public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string? query, string? category, decimal? priceMin, decimal? priceMax)
        {
            var products = await _repository.GetAllAsync();

            if (!string.IsNullOrEmpty(query))
            {
                products = products.Where(p => p.Name.ToLower().Contains(query.ToLower()));
            }


            if (!string.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.Category.ToLower().Contains(category.ToLower()));
            }

            if (priceMin.HasValue)
            {
                products = products.Where(p => p.Price >= priceMin.Value);
            }

            if (priceMax.HasValue)
            {
                products = products.Where(p => p.Price <= priceMax.Value);
            }

            return products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                Category = p.Category,
                Brand = p.Brand,
                Stock = p.Stock,
                CreatedAt = p.CreatedAt,
                ImageUrl = p.ImageUrl
            }).ToList();
    
        }



        private ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Brand = product.Brand,
                Price = product.Price,
                Category = product.Category,
                Stock = product.Stock,
                ImageUrl = product.ImageUrl,
                CreatedAt = product.CreatedAt
            };
        }
    }
}
