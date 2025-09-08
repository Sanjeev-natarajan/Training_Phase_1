using ShoppingApplication.Models;

namespace ShoppingApplication.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int userId);
    }
}
