    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using ShoppingApplication.Data;
    using ShoppingApplication.Models;
    using ShoppingApplication.Models.DTOs;

    namespace ShoppingApplication.Services
    {
        public class AuthService : IAuthService
        {
            private readonly AppDbContext _context;
            private readonly IConfiguration _config;
            private readonly IAuditLogService _auditLogService;
        public AuthService(AppDbContext context, IConfiguration config, IAuditLogService auditLogService)
        {
            _context = context;
            _config = config;
            _auditLogService = auditLogService;
        }

            public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
            {


            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = HashPassword(dto.Password),
                RoleId = 5,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber,
                City = dto.City ?? string.Empty, 
                ShopName = dto.ShopName,
                VehicleType = dto.VehicleType,
                LicenseNumber = dto.LicenseNumber,
                
            };



                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var savedUser = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserId == user.UserId);
                
                await _auditLogService.LogAsync("User Registered", savedUser.UserId, $"Registered with role {savedUser.Role.RoleName}");

                return GenerateAuthResponse(savedUser);
            }


        // private AuthResponseDto GenerateAuthResponse(User user)
        // {
        //     return new AuthResponseDto
        //     {
        //         UserId = user.UserId,
        //         Name = user.Name,
        //         Email = user.Email,
        //         Role = user.Role,
        //         Token = "sample-token"
        //     };
        // }

        public async Task<AuthResponseDto> RegisterShopkeeperAsync(RegisterDto dto)
        {
            dto.RoleId = 3;

            if (string.IsNullOrWhiteSpace(dto.ShopName))
                throw new Exception("Shop name is required for Shopkeeper.");

            var response = await RegisterWithRoleAsync(dto);

            await _auditLogService.LogAsync("Shopkeeper Registered", response.UserId, $"ShopName: {dto.ShopName}");

            return response;
    
        }



        public async Task<AuthResponseDto> RegisterDeliveryStaffAsync(RegisterDto dto)
        {
            dto.RoleId = 4;

            if (string.IsNullOrWhiteSpace(dto.VehicleType) || string.IsNullOrWhiteSpace(dto.LicenseNumber))
                throw new Exception("Vehicle type and license number are required for Delivery Staff.");

            var response = await RegisterWithRoleAsync(dto);

            await _auditLogService.LogAsync("Delivery Staff Registered", response.UserId, $"Vehicle: {dto.VehicleType}, License: {dto.LicenseNumber}");

            return response;
    
        }


        public async Task<AuthResponseDto> RegisterAdminAsync(RegisterDto dto, int createdByUserId)
        {
            var creator = await _context.Users.FirstOrDefaultAsync(u => u.UserId == createdByUserId);

            if (creator == null || creator.RoleId != 1)
                throw new Exception("Only Super Admin can create Admin accounts.");

            dto.RoleId = 2;
            var response = await RegisterWithRoleAsync(dto);

            await _auditLogService.LogAsync("Admin Registered", response.UserId, $"Created by SuperAdmin {createdByUserId}");

            return response;
    
        }

        private async Task<AuthResponseDto> RegisterWithRoleAsync(RegisterDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = HashPassword(dto.Password),
                RoleId = dto.RoleId,
                ShopName = dto.ShopName,
                VehicleType = dto.VehicleType,
                LicenseNumber = dto.LicenseNumber,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber,
                City = dto.City ?? string.Empty
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var savedUser = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == user.UserId);

            return GenerateAuthResponse(savedUser);
        }


        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null || !VerifyPassword(dto.Password, user.Password))
                throw new Exception("Invalid credentials");

            await _auditLogService.LogAsync("User Login", user.UserId, $"User {user.Email} logged in");


            return GenerateAuthResponse(user);
        }

            private bool VerifyPassword(string password, string storedHash)
            {
                var hash = HashPassword(password);
                return hash == storedHash;
            }

            private AuthResponseDto GenerateAuthResponse(User user)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.RoleName)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: creds
                );

                return new AuthResponseDto
                {
                    UserId = user.UserId,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    RoleId = user.RoleId,
                    Email = user.Email,
                    Name = user.Name
                };
            }

                private string HashPassword(string password)
            {
                using var sha256 = SHA256.Create();
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }

            
        }
        
    }