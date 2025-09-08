using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingApplication.Models.DTOs;
using ShoppingApplication.Services;

namespace ShoppingApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register/Customer")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                var result = await _authService.LoginAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("register/shopkeeper")]
        public async Task<IActionResult> RegisterShopkeeper(RegisterDto dto)
        {
            var result = await _authService.RegisterShopkeeperAsync(dto);
            return Ok(result);
        }


        [HttpPost("register/deliverystaff")]
        public async Task<IActionResult> RegisterDeliveryStaff(RegisterDto dto)
        {
            var result = await _authService.RegisterDeliveryStaffAsync(dto);
            return Ok(result);
        }


        [HttpPost("register/admin")]
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> RegisterAdmin(RegisterDto dto)
        {
            var superAdminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _authService.RegisterAdminAsync(dto, superAdminId);
            return Ok(result);
        }

    }
}