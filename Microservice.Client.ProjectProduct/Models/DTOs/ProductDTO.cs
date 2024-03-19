using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Microservice.Client.ProjectProduct.Models.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

       // [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

       // [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 1000.00, ErrorMessage = "Price must be between 0.01 and 1000.00")]
        public decimal Price { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }

        public string? CategoryName { get; set; }

       // [Required(ErrorMessage = "Quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a positive number")]
        public int Quantity { get; set; }

        public string? ImageUrl { get; set; }

        // Only one of these properties might be needed, depending on your requirements
         public string? ImageLocalPath { get; set; }
       // [Required(ErrorMessage = "Image is required")]
        public IFormFile? Image { get; set; }
    }
}
