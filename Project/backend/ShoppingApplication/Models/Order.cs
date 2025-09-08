using System.ComponentModel.DataAnnotations;

namespace ShoppingApplication.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int UserId { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending";

        public int? DeliveryPersonId { get; set; }

        public DateTime? DeliveredAt { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public User User { get; set; }
    }
}
