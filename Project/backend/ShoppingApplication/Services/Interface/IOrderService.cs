using ShoppingApplication.Models.DTOs;

namespace ShoppingApplication.Services
{
    public interface IOrderService
    {
        Task<OrderDto> PlaceOrderAsync(int userId, CreateOrderDto dto);
        Task<OrderDto?> GetOrderByIdAsync(int id);
        Task<OrderDto?> UpdateOrderStatusAsync(int orderId, string status);
        Task<List<OrderDto>> GetOrdersForDeliveryPersonAsync(int deliveryPersonId);
        Task<OrderDto?> AcceptOrRejectOrderAsync(int orderId, int deliveryPersonId, bool accept);
        Task<OrderDto?> AssignDeliveryPersonAsync(int orderId, int deliveryPersonId);
        Task<IEnumerable<OrderDto>> GetOrdersByStatusAsync(string status);
        Task<IEnumerable<OrderDto>> GetOrdersByDeliveryPersonAsync(int deliveryPersonId);
        Task<IEnumerable<OrderDto>> GetOrdersByUserAsync(int userId);
    }
}
