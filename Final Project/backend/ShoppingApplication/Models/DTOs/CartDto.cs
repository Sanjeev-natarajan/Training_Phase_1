namespace ShoppingApplication.Models.DTOs
{


    public class CartDto
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public List<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();
        public decimal TotalAmount => CartItems.Sum(i => i.Total);
    }
}
