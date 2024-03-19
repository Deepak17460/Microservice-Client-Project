using Microservice.Client.ProjectOrder.Models.DTOs;

namespace Microservice.Client.ProjectOrder.Services
{
    public interface IOrderService
    {
        public Task<OrderDTO> OrderPlaced(OrderDTO orderDTO);
    }
}
