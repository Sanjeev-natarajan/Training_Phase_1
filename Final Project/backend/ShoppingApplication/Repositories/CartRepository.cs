using Microsoft.EntityFrameworkCore;
using ShoppingApplication.Data;
using ShoppingApplication.Models;

namespace ShoppingApplication.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetUserCartAsync(int userId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.UserId == userId && o.Status == "Pending");

            return order ?? new Order { UserId = userId, Status = "Pending", OrderItems = new List<OrderItem>() };
        }

        public async Task AddItemAsync(int userId, int productId, int quantity)
        {
            var order = await GetUserCartAsync(userId);

            if (order.OrderId == 0)
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
            }

            var existingItem = order.OrderItems.FirstOrDefault(i => i.ProductId == productId);
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new Exception("Product not found");

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    Price = product.Price
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateItemAsync(int cartItemId, int quantity)
        {
            var item = await _context.Set<OrderItem>().FindAsync(cartItemId);
            if (item == null) throw new Exception("Cart item not found");

            item.Quantity = quantity;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveItemAsync(int cartItemId)
        {
            var item = await _context.Set<OrderItem>().FindAsync(cartItemId);
            if (item != null)
            {
                _context.Set<OrderItem>().Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
