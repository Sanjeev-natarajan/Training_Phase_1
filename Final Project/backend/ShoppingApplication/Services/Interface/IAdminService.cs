using ShoppingApplication.Models.DTOs;

namespace ShoppingApplication.Services
{
    public interface IAdminService
    {
        Task<AdminDashboardDto> GetDashboardDataAsync();
        Task<List<AdminUserDto>> GetUsersAsync();
        Task<List<AdminStoreDto>> GetStoresAsync();
        Task<bool> DeactivateUserAsync(int userId);
        Task<bool> ForceUpdateOrderAsync(int orderId, string newStatus);
        Task<List<AuditLogDto>> GetAuditLogsAsync();
    }
}
