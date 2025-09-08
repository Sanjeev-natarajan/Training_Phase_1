using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingApplication.Services;
using System.Security.Claims;

namespace ShoppingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "DeliveryPerson")]
    public class DeliveryController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IDeliveryService _deliveryService;

        public DeliveryController(IOrderService orderService, IDeliveryService deliveryService)
        {
            _orderService = orderService;
            _deliveryService = deliveryService;

        }

        private int GetDeliveryPersonId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(claim);
        }

        [HttpGet("MyOrders")]
        public async Task<IActionResult> GetMyOrders()
        {
            int deliveryPersonId = GetDeliveryPersonId();
            var orders = await _orderService.GetOrdersForDeliveryPersonAsync(deliveryPersonId);
            return Ok(orders);
        }

        [HttpPut("{orderId}/decision")]
        public async Task<IActionResult> AcceptOrReject(int orderId, [FromQuery] bool accept)
        {
            int deliveryPersonId = GetDeliveryPersonId();
            var updatedOrder = await _orderService.AcceptOrRejectOrderAsync(orderId, deliveryPersonId, accept);

            if (updatedOrder == null) return NotFound("Order not found.");
            return Ok(updatedOrder);
        }

         private int GetCurrentDeliveryStaffId()
        {
            var staffIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(staffIdClaim))
                throw new Exception("Delivery staff not authenticated");

            return int.Parse(staffIdClaim);
        }

       
        [HttpGet("my-history")]
        public async Task<IActionResult> GetMyHistory()
        {
            var staffId = GetCurrentDeliveryStaffId();
            var history = await _deliveryService.GetCompletedDeliveriesAsync(staffId);
            return Ok(history);
        }

        [HttpGet("routes/today")]
        public async Task<IActionResult> GetTodaysRoute()
        {
            var staffId = GetCurrentDeliveryStaffId();
            var route = await _deliveryService.GetTodaysRouteAsync(staffId);
            return Ok(route);
        }
    }
}
