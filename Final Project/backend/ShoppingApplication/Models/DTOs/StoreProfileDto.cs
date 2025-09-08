namespace ShoppingApplication.Models.DTOs
{

    public class StoreProfileDto
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string? LogoUrl { get; set; }
    }

}
