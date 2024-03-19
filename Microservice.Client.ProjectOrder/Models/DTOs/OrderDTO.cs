using System.ComponentModel.DataAnnotations;

namespace Microservice.Client.ProjectOrder.Models.DTOs
{
    public class OrderDTO
    {

        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Range(1, 100)]
        public int Quantity { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public double Price { get; set; }
    }
}
