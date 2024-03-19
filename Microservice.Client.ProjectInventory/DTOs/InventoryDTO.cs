namespace Microservice.Client.ProjectInventory.DTOs
{
    public class InventoryDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
