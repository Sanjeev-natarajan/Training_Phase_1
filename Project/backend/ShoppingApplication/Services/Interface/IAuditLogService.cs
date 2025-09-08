using ShoppingApplication.Models.DTOs;

namespace ShoppingApplication.Services
{
    public interface IAuditLogService
    {
        Task LogAsync(string action, int userId, string details);
        Task<List<AuditLogDto>> GetAuditLogsAsync();
    }
}
