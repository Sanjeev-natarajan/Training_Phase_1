using ShoppingApplication.Models;

namespace ShoppingApplication.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment> AddAsync(Payment payment);
        Task<Payment?> GetByIdAsync(int id);
        Task<Payment?> GetByOrderIdAsync(int orderId);
        Task<Payment> UpdateAsync(Payment payment);
    }
}
