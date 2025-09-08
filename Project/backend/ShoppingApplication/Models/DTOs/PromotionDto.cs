namespace ShoppingApplication.Models.DTOs
{

    public class PromotionDto
    {
        public int ProductId { get; set; }
        public decimal DiscountPercentage { get; set; } 
        public DateTime ValidUntil { get; set; }
    }


}
