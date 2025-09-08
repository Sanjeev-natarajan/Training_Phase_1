using Microsoft.EntityFrameworkCore;
using ShoppingApplication.Data;
using ShoppingApplication.Models;

namespace ShoppingApplication.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly AppDbContext _context;

        public DeliveryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DeliveryOrder>> GetCompletedDeliveriesAsync(int deliveryStaffId)
        {
            return await _context.DeliveryOrders
                                 .Where(d => d.DeliveryStaffId == deliveryStaffId && d.DeliveredAt != null)
                                 .ToListAsync();
        }

        public async Task<DeliveryRoute?> GetTodaysRouteAsync(int deliveryStaffId)
        {
            return await _context.DeliveryRoutes
                                 .Include(r => r.Orders)
                                 .FirstOrDefaultAsync(r => r.DeliveryStaffId == deliveryStaffId);
        }
    }
}
