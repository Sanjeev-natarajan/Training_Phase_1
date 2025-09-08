namespace ShoppingApplication.Models.DTOs
{

    public class DeliveryRouteDto
    {
        public int DeliveryStaffId { get; set; }
        public List<DeliveryOrderDto> Orders { get; set; } = new();
    }
}
