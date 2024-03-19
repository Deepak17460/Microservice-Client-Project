using Microservice.Client.ProjectInventory.DTOs;
using System;

namespace Microservice.Client.ProjectInventory.AppService
{
    public interface IInventoryService
    {
        public Task<InventoryDTO> UpdatedAddedProduct(InventoryDTO inventoryDTO);
        public Task<InventoryDTO> UpdatedupadtedProduct(InventoryDTO inventoryDTO);
        public Task<InventoryDTO> UpdatedDeletedProduct(int guid);

        public Task<InventoryDTO> GetProductbyGuid(int guid);

        public Task<InventoryDTO> GetOrderProductbyGuid(OrderDTO orderDTO);

    }
}
