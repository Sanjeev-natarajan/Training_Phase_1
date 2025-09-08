using ShoppingApplication.Models;
using ShoppingApplication.Models.DTOs;

namespace ShoppingApplication.Repositories
{
    public interface IAdminRepository
    {
        Task<AdminDashboardDto> GetDashboardDataAsync();
        Task<List<User>> GetUsersAsync();
        Task<List<User>> GetStoresAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<bool> DeactivateUserAsync(User user);
        Task<Order?> GetOrderByIdAsync(int id);
        Task<bool> ForceUpdateOrderAsync(Order order, string newStatus);
        Task<List<AuditLogDto>> GetAuditLogsAsync();
    }
}
