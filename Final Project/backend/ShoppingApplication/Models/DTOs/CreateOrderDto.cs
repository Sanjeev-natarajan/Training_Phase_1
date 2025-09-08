namespace ShoppingApplication.Models.DTOs
{
    public class CreateOrderDto
    {
        public int StorekeeperId { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }

   
}