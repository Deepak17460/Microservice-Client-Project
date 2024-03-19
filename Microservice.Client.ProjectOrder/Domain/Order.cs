using System.ComponentModel.DataAnnotations;

namespace Microservice.Client.ProjectOrder.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
