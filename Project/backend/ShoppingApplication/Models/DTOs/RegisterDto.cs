using System.ComponentModel.DataAnnotations;

namespace ShoppingApplication.Models.DTOs
{
    public class RegisterDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [MaxLength(100)]
        public string? ShopName { get; set; }

          [MaxLength(50)]
        public string? VehicleType { get; set; }

        [MaxLength(50)]
        public string? LicenseNumber { get; set; }
        
        [Required, MaxLength(200)] 
        public string Address { get; set; }

        public string City { get; set; }

        public int RoleId { get; set; } 
    }
}