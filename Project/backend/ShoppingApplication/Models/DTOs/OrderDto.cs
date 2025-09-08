namespace ShoppingApplication.Models.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public int? DeliveryPersonId { get; set; }

        public decimal TotalAmount { get; set; }
        public List<OrderItemDetailDto> Items { get; set; }
    }


}
