using ShoppingApplication.Models;
using ShoppingApplication.Models.DTOs;

namespace ShoppingApplication.Services
{
    public interface IPaymentService
    {
        Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto dto);
        Task<PaymentDto?> MarkAsPaidAsync(int orderId);
        Task<PaymentDto?> GetPaymentByOrderIdAsync(int orderId);
    }
}
