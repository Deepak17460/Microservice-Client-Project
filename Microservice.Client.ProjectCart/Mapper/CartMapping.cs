using AutoMapper;
using Microservice.Client.ProjectCart.Domain;
using Microservice.Client.ProjectCart.Models.DTOs;

namespace Microservice.Client.ProjectCart.Mapper
{
    public class CartMapping:Profile
    {
        public CartMapping():base("CartMappig")
        {
            CreateMap<CartDTO,Cart>().ReverseMap();
        }
    }
}
