using ShoppingApplication.Models.DTOs;

namespace ShoppingApplication.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
        Task<AuthResponseDto> RegisterShopkeeperAsync(RegisterDto dto);
        Task<AuthResponseDto> RegisterDeliveryStaffAsync(RegisterDto dto);
        Task<AuthResponseDto> RegisterAdminAsync(RegisterDto dto, int createdByUserId);

    }
}