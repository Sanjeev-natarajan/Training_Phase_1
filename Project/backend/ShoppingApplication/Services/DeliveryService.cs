using ShoppingApplication.Models.DTOs;
using ShoppingApplication.Repositories;

namespace ShoppingApplication.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IDeliveryRepository _repository;
        private readonly IAuditLogService _auditLogService;


        public DeliveryService(IDeliveryRepository repository, IAuditLogService auditLogService)
        {
            _repository = repository;
            _auditLogService = auditLogService;
        }

        
        public async Task<IEnumerable<DeliveryHistoryDto>> GetCompletedDeliveriesAsync(int deliveryStaffId)
        {
            var deliveries = await _repository.GetCompletedDeliveriesAsync(deliveryStaffId);

            await _auditLogService.LogAsync("View Completed Deliveries", deliveryStaffId,
        $"Viewed {deliveries.Count()} completed deliveries");

            return deliveries.Select(d => new DeliveryHistoryDto
            {
                OrderId = d.OrderId,
                CustomerName = d.CustomerName,
                Address = d.Address,
                DeliveredAt = d.DeliveredAt
            });
        }

      
        public async Task<DeliveryRouteDto?> GetTodaysRouteAsync(int deliveryStaffId)
        {
            var route = await _repository.GetTodaysRouteAsync(deliveryStaffId);
            if (route == null) return null;
             await _auditLogService.LogAsync("Get Today’s Route", deliveryStaffId,
        $"Fetched today’s route with {route.Orders.Count} orders");

            return new DeliveryRouteDto
            {
                DeliveryStaffId = deliveryStaffId,
                Orders = route.Orders.Select(o => new DeliveryOrderDto
                {
                    OrderId = o.OrderId,
                    CustomerName = o.CustomerName,
                    Address = o.Address,
                    Status = o.Status,
                    DeliveredAt = o.DeliveredAt
                }).ToList()
            };
        }
    }
}
