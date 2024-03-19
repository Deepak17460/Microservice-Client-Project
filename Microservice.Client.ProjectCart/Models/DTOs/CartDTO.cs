namespace Microservice.Client.ProjectCart.Models.DTOs
{
    public class CartDTO
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public string? UserId { get; set; }

        public double Price { get; set; }
    }
}
