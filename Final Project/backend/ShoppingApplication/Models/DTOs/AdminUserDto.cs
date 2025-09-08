namespace ShoppingApplication.Models.DTOs
{
    public class AdminUserDto
    {
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string RoleName { get; set; } = null!;

        public bool IsActive { get; set; }
    }
}
