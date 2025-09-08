using ShoppingApplication.Models;
using ShoppingApplication.Models.DTOs;
using ShoppingApplication.Repositories;
using ShoppingApplication.Repository;

namespace ShoppingApplication.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;

        public PaymentService(IPaymentRepository paymentRepository, IOrderRepository orderRepository)
        {
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
        }

        public async Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto dto)
        {
            var order = await _orderRepository.GetByIdAsync(dto.OrderId);
            if (order == null) throw new Exception("Order not found");

            var payment = new Payment
            {
                OrderId = dto.OrderId,
                Amount = dto.Amount,
                Status = "Pending"
            };

            var saved = await _paymentRepository.AddAsync(payment);

            return MapToDto(saved);
        }

        public async Task<PaymentDto?> MarkAsPaidAsync(int orderId)
        {
            var payment = await _paymentRepository.GetByOrderIdAsync(orderId);
            if (payment == null) return null;

            payment.Status = "Completed";
            payment.PaidAt = DateTime.UtcNow;

            await _paymentRepository.UpdateAsync(payment);

            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order != null)
            {
                order.Status = "Paid";
                await _orderRepository.UpdateAsync(order);
            }

            return MapToDto(payment);
        }

        public async Task<PaymentDto?> GetPaymentByOrderIdAsync(int orderId)
        {
            var payment = await _paymentRepository.GetByOrderIdAsync(orderId);
            return payment == null ? null : MapToDto(payment);
        }

        private PaymentDto MapToDto(Payment payment)
        {
            return new PaymentDto
            {
                PaymentId = payment.PaymentId,
                OrderId = payment.OrderId,
                Amount = payment.Amount,
                Status = payment.Status,
                CreatedAt = payment.CreatedAt,
                PaidAt = payment.PaidAt
            };
        }
    }
}
