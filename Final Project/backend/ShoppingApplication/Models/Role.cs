using System.ComponentModel.DataAnnotations;

namespace ShoppingApplication.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; } 

        [Required]
        public string RoleName { get; set; } 
    }
}
