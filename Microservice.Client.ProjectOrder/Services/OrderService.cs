using AutoMapper;
using Microservice.Client.ProjectOrder.Domain;
using Microservice.Client.ProjectOrder.Models.DTOs;
using Microservice.Client.ProjectOrder.Repository;

namespace Microservice.Client.ProjectOrder.Services
{
    public class OrderService:IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepository orderRepository,IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<OrderDTO> OrderPlaced(OrderDTO orderDTO)
        {
            var order=_mapper.Map<OrderDTO,Order>(orderDTO);
            return await _orderRepository.GetPlacedOrder(order);
        }
    }
}
