using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingApplication.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required, MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string? ShopName { get; set; }

        [MaxLength(50)]
        public string? VehicleType { get; set; }

        [MaxLength(50)]
        public string? LicenseNumber { get; set; }

        

         public string City { get; set; } = string.Empty;
                 public bool IsActive { get; set; } = true;



        [Required]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;




        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}