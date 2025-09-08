using Microsoft.EntityFrameworkCore;
using ShoppingApplication.Data;
using ShoppingApplication.Models;
using ShoppingApplication.Models.DTOs;

namespace ShoppingApplication.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly AppDbContext _context;

        public AuditLogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AuditLog log)
        {
            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AuditLogDto>> GetAuditLogsAsync()
        {
            return await _context.AuditLogs
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
        }
    }
}
