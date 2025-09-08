namespace ShoppingApplication.Models.DTOs
{
    public class AuditLogDto
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Details { get; set; }
    }


}
