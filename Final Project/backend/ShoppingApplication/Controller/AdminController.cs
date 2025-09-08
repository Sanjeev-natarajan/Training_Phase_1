using Microsoft.AspNetCore.Mvc;
using ShoppingApplication.Models.DTOs;
using ShoppingApplication.Services;

namespace ShoppingApplication.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _service;
        public AdminController(IAdminService service)
        {
            _service = service;
        }

        [HttpGet("overview")]
        public async Task<ActionResult<AdminDashboardDto>> GetOverview() => Ok(await _service.GetDashboardDataAsync());

        [HttpGet("stores")]
        public async Task<ActionResult<List<AdminStoreDto>>> GetStores() => Ok(await _service.GetStoresAsync());

        [HttpGet("users")]
        public async Task<ActionResult<List<AdminUserDto>>> GetUsers() => Ok(await _service.GetUsersAsync());

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeactivateUser(int id)
        {
            var result = await _service.DeactivateUserAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }



          [HttpGet("audit-logs")]
    public async Task<ActionResult<List<AuditLogDto>>> GetAuditLogs()
    {
        var logs = await _service.GetAuditLogsAsync();
        return Ok(logs);
    }
    
        [HttpPut("orders/{id}/force-update")]
        public async Task<IActionResult> ForceUpdateOrder(int id, [FromQuery] string newStatus)
        {
            var result = await _service.ForceUpdateOrderAsync(id, newStatus);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
