using ShoppingApplication.Models.DTOs;

namespace ShoppingApplication.Services
{
    public interface IDeliveryService
    {
        Task<IEnumerable<DeliveryHistoryDto>> GetCompletedDeliveriesAsync(int deliveryStaffId);

        Task<DeliveryRouteDto?> GetTodaysRouteAsync(int deliveryStaffId);
    }
}
