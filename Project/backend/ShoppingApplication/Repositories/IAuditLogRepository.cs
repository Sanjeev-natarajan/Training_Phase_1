using ShoppingApplication.Models;
using ShoppingApplication.Models.DTOs;

namespace ShoppingApplication.Repositories
{
    public interface IAuditLogRepository
    {
        Task AddAsync(AuditLog log);
        Task<List<AuditLogDto>> GetAuditLogsAsync();
    }
}
