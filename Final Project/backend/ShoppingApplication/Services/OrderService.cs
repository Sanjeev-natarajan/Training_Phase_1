using ShoppingApplication.Models;
using ShoppingApplication.Models.DTOs;
using ShoppingApplication.Repositories;
using ShoppingApplication.Repository;

namespace ShoppingApplication.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IAuditLogService _auditLogService;
        private readonly IUserRepository _userRepository;

        public OrderService(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            IAuditLogService auditLogService, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _auditLogService = auditLogService;
            _userRepository = userRepository;
        }

        public async Task<OrderDto> PlaceOrderAsync(int userId, CreateOrderDto dto)
        {
            var customer = await _userRepository.GetByIdAsync(userId);
            if (customer == null) throw new Exception("Customer not found");

            var storekeeper = await _userRepository.GetByIdAsync(dto.StorekeeperId);
            if (storekeeper == null) throw new Exception("Storekeeper not found");

            if (!string.Equals(customer.City, storekeeper.City, StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception($"Delivery not available. Customer is in {customer.City}, but store is in {storekeeper.City}");
            }

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                Status = "Pending",
                OrderItems = new List<OrderItem>()
            };

            foreach (var item in dto.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product == null || product.Stock < item.Quantity)
                    throw new Exception($"Product {item.ProductId} not available or insufficient stock.");

                product.Stock -= item.Quantity;

                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = product.Price
                });
            }

            var savedOrder = await _orderRepository.AddOrderAsync(order);

            await _auditLogService.LogAsync("PlaceOrder", userId,
                $"Placed order #{savedOrder.OrderId} with {savedOrder.OrderItems.Count} items");

            return MapToDto(savedOrder);
        }


        public async Task<IEnumerable<OrderDto>> GetOrdersByStatusAsync(string status)
        {
            var orders = await _orderRepository.GetOrdersByStatusAsync(status);

            await _auditLogService.LogAsync("GetOrdersByStatus", 0,
                $"Fetched {orders.Count()} orders with status '{status}'");

            return orders.Select(MapToDto);
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);

            if (order != null)
            {
                await _auditLogService.LogAsync("GetOrderById", order.UserId,
                    $"Fetched details for order #{order.OrderId}");
            }

            return order == null ? null : MapToDto(order);
        }

        public async Task<OrderDto?> UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _orderRepository.UpdateOrderStatusAsync(orderId, status);

            if (order != null)
            {
                await _auditLogService.LogAsync("UpdateOrderStatus", order.UserId,
                    $"Order #{order.OrderId} status updated to {status}");
            }

            return order == null ? null : MapToDto(order);
        }

        public async Task<List<OrderDto>> GetOrdersForDeliveryPersonAsync(int deliveryPersonId)
        {
            var orders = await _orderRepository.GetOrdersByDeliveryPersonAsync(deliveryPersonId);

            await _auditLogService.LogAsync("GetOrdersForDeliveryPerson", deliveryPersonId,
                $"Fetched {orders.Count()} assigned orders for delivery");

            return orders.Select(MapToDto).ToList();
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByDeliveryPersonAsync(int deliveryPersonId)
        {
            var orders = await _orderRepository.GetOrdersByDeliveryPersonAsync(deliveryPersonId);

            await _auditLogService.LogAsync("GetOrdersByDeliveryPerson", deliveryPersonId,
                $"Fetched {orders.Count()} assigned orders");

            return orders.Select(MapToDto);
        }

        public async Task<OrderDto?> AcceptOrRejectOrderAsync(int orderId, int deliveryPersonId, bool accept)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null) return null;

            order.DeliveryPersonId = deliveryPersonId;
            order.Status = accept ? "OutForDelivery" : "RejectedByDelivery";

            var updated = await _orderRepository.UpdateAsync(order);

            await _auditLogService.LogAsync("AcceptOrRejectOrder", deliveryPersonId,
                $"{(accept ? "Accepted" : "Rejected")} order #{order.OrderId}");

            return MapToDto(updated);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByUserAsync(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);

            await _auditLogService.LogAsync("GetOrdersByUser", userId,
                $"Fetched {orders.Count()} orders for user {userId}");

            return orders.Select(MapToDto);
        }

        public async Task<OrderDto?> AssignDeliveryPersonAsync(int orderId, int deliveryPersonId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null) return null;

            order.DeliveryPersonId = deliveryPersonId;
            var updated = await _orderRepository.UpdateAsync(order);

            await _auditLogService.LogAsync("AssignDeliveryPerson", 0,
                $"Assigned delivery person {deliveryPersonId} to order #{order.OrderId}");

            return MapToDto(updated);
        }

        private static OrderDto MapToDto(Order order)
        {
            return new OrderDto
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                Status = order.Status,
                DeliveryPersonId = order.DeliveryPersonId,
                TotalAmount = order.OrderItems.Sum(i => i.Quantity * i.Price),
                Items = order.OrderItems.Select(oi => new OrderItemDetailDto
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product?.Name ?? "",
                    Price = oi.Price,
                    Quantity = oi.Quantity
                }).ToList()
            };
        }
    }
}
