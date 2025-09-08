using Microsoft.AspNetCore.Mvc;
using ShoppingApplication.Models.DTOs;
using ShoppingApplication.Services;

namespace ShoppingApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentDto dto)
        {
            var payment = await _paymentService.CreatePaymentAsync(dto);
            return Ok(payment);
        }

        [HttpPost("pay/{orderId}")]
        public async Task<IActionResult> PayOrder(int orderId)
        {
            var payment = await _paymentService.MarkAsPaidAsync(orderId);
            if (payment == null) return NotFound("Payment not found");
            return Ok(payment);
        }

        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetPaymentByOrder(int orderId)
        {
            var payment = await _paymentService.GetPaymentByOrderIdAsync(orderId);
            if (payment == null) return NotFound();
            return Ok(payment);
        }
    }
}
