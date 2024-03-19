using AutoMapper;
using Microservice.Client.ProjectInventory.DTOs;
using Microservice.Client.ProjectInventory.InventoryModel;

namespace Microservice.Client.ProjectInventory.Mapper
{
    public class InventoryMapping:Profile
    {
        public InventoryMapping():base("InventoryMapping")
        {
            CreateMap<InventoryDTO,Inventory>().ReverseMap();
        }
    }
}
