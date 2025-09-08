namespace ShoppingApplication.Models.DTOs
{
    public class AdminStoreDto
    {
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string ShopName { get; set; } = null!;
        public string Address { get; set; } = null!;
    }
}
