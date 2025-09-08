using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApplication.Models
{
    public class DeliveryOrder
    {
        [Key]
        public int OrderId { get; set; }
        public int DeliveryStaffId { get; set; }
        public string CustomerName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Status { get; set; } = "Pending";
        public DateTime? DeliveredAt { get; set; }
        

    // public int? DeliveryRouteRouteId { get; set; }
    
    //     public DeliveryRoute? DeliveryRoute { get; set; }
    }
}
