namespace ShoppingApplication.Models.DTOs
{

    public class DeliveryOrderDto
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime? DeliveredAt { get; set; }
    }
}
