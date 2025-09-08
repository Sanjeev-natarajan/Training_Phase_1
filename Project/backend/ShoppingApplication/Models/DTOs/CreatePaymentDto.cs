namespace ShoppingApplication.Models.DTOs
{


    public class CreatePaymentDto
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
    }
}
