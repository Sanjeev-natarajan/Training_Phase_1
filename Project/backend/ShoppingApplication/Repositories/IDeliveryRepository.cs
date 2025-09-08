using ShoppingApplication.Models;

namespace ShoppingApplication.Repositories
{
    public interface IDeliveryRepository
    {
        Task<IEnumerable<DeliveryOrder>> GetCompletedDeliveriesAsync(int deliveryStaffId);

        Task<DeliveryRoute?> GetTodaysRouteAsync(int deliveryStaffId);
    }
}
