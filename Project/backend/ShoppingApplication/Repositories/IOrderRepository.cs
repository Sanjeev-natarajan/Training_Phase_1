using ShoppingApplication.Models;

namespace ShoppingApplication.Repository
{
    public interface IOrderRepository
    {
        Task<Order> AddOrderAsync(Order order);
        Task<Order?> GetOrderByIdAsync(int id);

        Task<Order?> UpdateOrderStatusAsync(int orderId, string status);
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
        Task<List<Order>> GetOrdersByDeliveryPersonAsync(int deliveryPersonId);
        Task<IEnumerable<Order>> GetOrdersByStatusAsync(string status);
        Task<Order?> GetByIdAsync(int id);
        Task<Order?> UpdateAsync(Order order);

        Task SaveChangesAsync();
    }
}
