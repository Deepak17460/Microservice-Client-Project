using Microservice.Client.ProjectOrder.Domain;
using Microservice.Client.ProjectOrder.Models.DTOs;

namespace Microservice.Client.ProjectOrder.Repository
{
    public interface IOrderRepository
    {
        public Task<OrderDTO> GetPlacedOrder(Order order);
    }
}
