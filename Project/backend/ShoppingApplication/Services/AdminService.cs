using ShoppingApplication.Models.DTOs;
using ShoppingApplication.Repositories;

namespace ShoppingApplication.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repository;
        public AdminService(IAdminRepository repository)
        {
            _repository = repository;
        }

        public Task<AdminDashboardDto> GetDashboardDataAsync() => _repository.GetDashboardDataAsync();

        public async Task<List<AdminUserDto>> GetUsersAsync()
        {
            var users = await _repository.GetUsersAsync();
            return users.Select(u => new AdminUserDto
            {
                UserId = u.UserId,
                Name = u.Name,
                Email = u.Email,
                RoleName = u.Role.RoleName,
                IsActive = u.IsActive
            }).ToList();
        }

        public async Task<List<AdminStoreDto>> GetStoresAsync()
        {
            var stores = await _repository.GetStoresAsync();
            return stores.Select(s => new AdminStoreDto
            {
                UserId = s.UserId,
                Name = s.Name,
                ShopName = s.ShopName ?? "",
                Address = s.Address
            }).ToList();
        }

        public async Task<bool> DeactivateUserAsync(int userId)
        {
            var user = await _repository.GetUserByIdAsync(userId);
            if (user == null) return false;
            return await _repository.DeactivateUserAsync(user);
        }

        public Task<bool> ForceUpdateOrderAsync(int orderId, string newStatus) =>
            _repository.ForceUpdateOrderAsync(_repository.GetOrderByIdAsync(orderId).Result, newStatus);

        public async Task<List<AuditLogDto>> GetAuditLogsAsync()
        {
            return await _repository.GetAuditLogsAsync();
        }
    
    }
}
