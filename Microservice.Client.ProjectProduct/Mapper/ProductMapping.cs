using AutoMapper;
using Microservice.Client.ProjectProduct.Domain;
using Microservice.Client.ProjectProduct.Models.DTOs;

namespace Microservice.Client.ProjectProduct.Mapper
{
    public class ProductMapping:Profile
    {
        public ProductMapping():base("ProductMapping")
        {
            CreateMap<ProductDTO,Product>().ReverseMap();
            
        }
    }
}
