using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApplication.Models
{
    public class DeliveryRoute
    {
        [Key]
        public int RouteId { get; set; }
        public int DeliveryStaffId { get; set; }
        public List<DeliveryOrder> Orders { get; set; } = new();
    }
}
