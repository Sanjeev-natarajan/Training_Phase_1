using Microsoft.AspNetCore.Mvc;

namespace ShoppingApplication.Models.DTOs
{
    public class CreateProductDto
    {

        public string Name { get; set; }
    
        public string Brand { get; set; }
  
        public decimal Price { get; set; }
      
        public string Category { get; set; }
    
        public int Stock { get; set; }
       
        public IFormFile? Image { get; set; } 
        

    }
}