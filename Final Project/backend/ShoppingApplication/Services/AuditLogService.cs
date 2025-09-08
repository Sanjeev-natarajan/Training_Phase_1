using ShoppingApplication.Models;
using ShoppingApplication.Models.DTOs;
using ShoppingApplication.Repositories;

namespace ShoppingApplication.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IAuditLogRepository _repository;

        public AuditLogService(IAuditLogRepository repository)
        {
            _repository = repository;
        }

        public async Task LogAsync(string action, int userId, string details)
        {
            var log = new AuditLog
            {
                Action = action,
                UserId = userId,
                CreatedAt = DateTime.Now,
                Details = details
            };

            await _repository.AddAsync(log);
        }

        public async Task<List<AuditLogDto>> GetAuditLogsAsync()
        {
            return await _repository.GetAuditLogsAsync();
        }
    }
}
