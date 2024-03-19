using AutoMapper;
using Microservice.Client.ProjectInventory.DataDb;
using Microservice.Client.ProjectInventory.DTOs;
using Microservice.Client.ProjectInventory.InventoryModel;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Client.ProjectInventory.AppService
{
    public class InventoryService:IInventoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public InventoryService(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;   
        }
        public async Task<InventoryDTO> UpdatedAddedProduct(InventoryDTO inventoryDTO)
        {
            var inventory=_mapper.Map<InventoryDTO,Inventory>(inventoryDTO);
             Inventory _inventory=new Inventory()
             {
                 ProductId = inventory.ProductId,
                 Price = inventory.Price,
                 Quantity = inventory.Quantity,

             };
            await _context.Inventories.AddAsync(inventory);
            await _context.SaveChangesAsync();
            return _mapper.Map<Inventory, InventoryDTO>(_inventory);
        }

        public async Task<InventoryDTO> UpdatedupadtedProduct(InventoryDTO inventoryDTO)
        {
            var inventory = _mapper.Map<InventoryDTO, Inventory>(inventoryDTO);
            Inventory _inventory = new Inventory()
            {
                ProductId = inventory.ProductId,
                Price = inventory.Price,
                Quantity = inventory.Quantity,

            };
            await _context.SaveChangesAsync();
           return _mapper.Map<Inventory, InventoryDTO>(_inventory);
        }
        public async Task<InventoryDTO> UpdatedDeletedProduct(int guid)
        {
            var inventory=await _context.Inventories.FirstOrDefaultAsync(I=>I.ProductId==guid);
            if (inventory != null)
            {
                _context.Inventories.Remove(inventory);
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<Inventory, InventoryDTO>(inventory);
        }
        public async Task<InventoryDTO> GetProductbyGuid(int guid)
        {
            var inventory= await _context.Inventories.FirstOrDefaultAsync(p=>p.ProductId==guid);
            return _mapper.Map<Inventory, InventoryDTO>(inventory);
        }
        public async Task<InventoryDTO> GetOrderProductbyGuid(OrderDTO orderDTO)
        {
            var product=await _context.Inventories.FirstOrDefaultAsync(p=>p.ProductId==orderDTO.ProductId);
            if(product != null)
            {
                product.Quantity = orderDTO.Quantity;
                await _context.SaveChangesAsync();
            }
            return _mapper.Map<Inventory,InventoryDTO>(product);
        }
    }
}
