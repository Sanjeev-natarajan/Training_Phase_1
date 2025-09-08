using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingApplication.Models.DTOs;
using ShoppingApplication.Services;
using System.Security.Claims;

namespace ShoppingApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("PlaceOrder")]
        public async Task<IActionResult> PlaceOrder(CreateOrderDto dto)
        {
            var userId = GetUserId();
            var order = await _orderService.PlaceOrderAsync(userId, dto);
            return Ok(order);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("MyOrders")]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = GetUserId();
            var orders = await _orderService.GetOrdersByUserAsync(userId);
            return Ok(orders);
        }

        [Authorize(Roles = "StoreKeeper")]
        [HttpPut("{orderId}/assign/{deliveryPersonId}")]
        public async Task<IActionResult> AssignDeliveryPerson(int orderId, int deliveryPersonId)
        {
            var order = await _orderService.AssignDeliveryPersonAsync(orderId, deliveryPersonId);
            if (order == null) return NotFound("Order not found.");
            return Ok(order);
        }

        [Authorize(Roles = "DeliveryPerson")]
        [HttpGet("MyAssignedOrders")]
        public async Task<IActionResult> GetMyAssignedOrders()
        {
            var deliveryPersonId = GetUserId();
            var orders = await _orderService.GetOrdersByDeliveryPersonAsync(deliveryPersonId);
            return Ok(orders);
        }

        [Authorize(Roles = "StoreKeeper,DeliveryPerson")]
        [HttpPut("{orderId}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] string status)
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (role == "StoreKeeper")
            {
                var allowedStatuses = new[] { "Pending", "Accepted", "Rejected", "Dispatched" };
                if (!allowedStatuses.Contains(status))
                    return BadRequest($"StoreKeeper can only set: {string.Join(", ", allowedStatuses)}");
            }

            if (role == "DeliveryPerson")
            {
                var allowedStatuses = new[] { "OutForDelivery", "Delivered", "Customer Unavailable" };
                if (!allowedStatuses.Contains(status))
                    return BadRequest($"DeliveryPerson can only set: {string.Join(", ", allowedStatuses)}");
            }

            var updatedOrder = await _orderService.UpdateOrderStatusAsync(orderId, status);
            if (updatedOrder == null) return NotFound();
            

            return Ok(updatedOrder);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpGet("status/{status}")]
        [Authorize(Roles = "StoreKeeper,Customer")]
        public async Task<IActionResult> GetOrdersByStatus(string status)
        {
            var orders = await _orderService.GetOrdersByStatusAsync(status);
            return Ok(orders);
        }
    }
}
