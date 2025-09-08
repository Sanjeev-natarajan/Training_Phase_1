using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ShoppingApplication.Models.DTOs;
using ShoppingApplication.Services;

namespace ShoppingApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IWebHostEnvironment _env;

        public ProductsController(IProductService service, IWebHostEnvironment env)
        {
            _service = service;
            _env = env;
        }

        [HttpGet("Getall")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("GetbyId/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpGet("GetbyName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var product = await _service.GetProductByName(name);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Create([FromForm] CreateProductDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null) return Unauthorized("User ID not found");

            int userId = int.Parse(userIdClaim);

            var uploadPath = Path.Combine(_env.WebRootPath, "uploads");

            var product = await _service.CreateProductAsync(dto, uploadPath, userId);

            return CreatedAtAction(nameof(GetById), new { id = product.ProductId }, product);
        }


        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] CreateProductDto dto)
        {
            var uploadPath = Path.Combine(_env.WebRootPath, "uploads");
            var product = await _service.UpdateProductAsync(id, dto, uploadPath);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteProductAsync(id);
            if (!success) return NotFound();
            return Ok("Product Deleted");
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> SearchProducts(
    [FromQuery] string? query,
    [FromQuery] string? category,
    [FromQuery] decimal? priceMin,
    [FromQuery] decimal? priceMax)
        {
            var products = await _service.SearchProductsAsync(query, category, priceMin, priceMax);

            var result = products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                Category = p.Category,
                Brand = p.Brand,
                Stock = p.Stock,
                CreatedAt = p.CreatedAt,
                ImageUrl = string.IsNullOrEmpty(p.ImageUrl)
                           ? null
                           : $"{Request.Scheme}://{Request.Host}{p.ImageUrl}"
            }).ToList();

            return Ok(result);
        }




    }
}
