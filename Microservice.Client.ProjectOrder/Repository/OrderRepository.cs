using AutoMapper;
using Microservice.Client.ProjectOrder.DataDb;
using Microservice.Client.ProjectOrder.Domain;
using Microservice.Client.ProjectOrder.Models.DTOs;

namespace Microservice.Client.ProjectOrder.Repository
{
    public class OrderRepository:IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public OrderRepository(ApplicationDbContext context,IMapper mapper)
        {
           _context=context;
            _mapper = mapper;
        }
        public async Task<OrderDTO> GetPlacedOrder(Order order)
        {
            order.Price = (order.Price * order.Quantity);
            var res=await _context.Orders.AddAsync(order);
            _context.SaveChanges();
            return _mapper.Map<Order,OrderDTO>(order);
        }
    }
}
