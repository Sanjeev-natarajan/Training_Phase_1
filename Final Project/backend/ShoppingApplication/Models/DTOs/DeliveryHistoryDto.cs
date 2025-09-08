namespace ShoppingApplication.Models.DTOs
{
    public class DeliveryHistoryDto
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime? DeliveredAt { get; set; }
    }




}
