using Microsoft.EntityFrameworkCore;
using ShoppingApplication.Data;
using ShoppingApplication.Models;
using ShoppingApplication.Models.DTOs;

namespace ShoppingApplication.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;
        public AdminRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AdminDashboardDto> GetDashboardDataAsync()
        {
            return new AdminDashboardDto
            {
                ActiveOrders = await _context.Orders.CountAsync(o => o.Status == "Pending"),
                UsersCount = await _context.Users.CountAsync(),
                StoresCount = await _context.Users.Include(u => u.Role)
                                    .CountAsync(u => u.Role.RoleName == "Store"),
                DeliveryStaffCount = await _context.Users.Include(u => u.Role)
                                    .CountAsync(u => u.Role.RoleName == "Delivery")
            };
        }

        public Task<List<User>> GetUsersAsync() => _context.Users.Include(u => u.Role).ToListAsync();

        public Task<List<User>> GetStoresAsync() => _context.Users.Include(u => u.Role)
                                    .Where(u => u.Role.RoleName == "Store").ToListAsync();

        public Task<User?> GetUserByIdAsync(int id) => _context.Users.Include(u => u.Role)
                                    .FirstOrDefaultAsync(u => u.UserId == id);

        public async Task<bool> DeactivateUserAsync(User user)
        {
            user.IsActive = false; 
            _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }


        public Task<Order?> GetOrderByIdAsync(int id) => _context.Orders.FindAsync(id).AsTask();

        public async Task<bool> ForceUpdateOrderAsync(Order order, string newStatus)
        {
            order.Status = newStatus;
            _context.Orders.Update(order);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<AuditLogDto>> GetAuditLogsAsync()
    {
        var logs = await _context.AuditLogs
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new AuditLogDto
            {
                Id = x.Id,
                Action = x.Action,
                UserId = x.UserId,
                CreatedAt = x.CreatedAt,
                Details = x.Details
            })
            .ToListAsync();

        return logs;
    }
    }
}
