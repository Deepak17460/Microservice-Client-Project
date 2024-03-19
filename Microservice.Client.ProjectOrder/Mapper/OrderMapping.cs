using AutoMapper;
using Microservice.Client.ProjectOrder.Domain;
using Microservice.Client.ProjectOrder.Models.DTOs;

namespace Microservice.Client.ProjectOrder.Mapper
{
    public class OrderMapping:Profile
    {
        public OrderMapping():base("OrderMapping")
        { 
            CreateMap<OrderDTO,Order>().ReverseMap();
        }
    }

}
