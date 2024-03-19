using System.ComponentModel.DataAnnotations;

namespace Microservice.Client.ProjectProduct.Domain
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1, 1000)]
        public double Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        [Required]
        [Range(0, Int32.MaxValue - 1)]
        public int Quantity { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageLocalPath { get; set; }
    }
}