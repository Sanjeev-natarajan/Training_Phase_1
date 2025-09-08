using System.ComponentModel.DataAnnotations;

namespace ShoppingApplication.Models.DTOs
{
    public class LoginDto
    {
        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; }
        
        [Required, MaxLength(100)]
        public string Password { get; set; }
    }
}