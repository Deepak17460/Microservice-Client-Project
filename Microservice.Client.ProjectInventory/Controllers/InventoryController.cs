using Microservice.Client.ProjectInventory.AppService;
using Microservice.Client.ProjectInventory.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Client.ProjectInventory.Controllers
{
    [Route("inventory")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        private readonly ResponseDTO _responseDTO;
        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
            _responseDTO = new ResponseDTO();
        }
        //Get-Addeded-Product Information
        [HttpPost]
        [Route("product-added-info")]
        public async Task<IActionResult> GetAddedProductInfo([FromBody] InventoryDTO inventoryDTO)
        {
            var res = await _inventoryService.UpdatedAddedProduct(inventoryDTO);
            if(res ==null) {
                return NotFound("An Error Occured While Updateding Product in Inventory!");
            }
            _responseDTO.IsSuccess = true;
            _responseDTO.Message = "Added Product Information in Inventor Successfully!";
            _responseDTO.Result = res;
            return Ok(res);

        }
        //Get-Updated-Product Information
        [HttpPut]
        [Route("product-updated-info")]
        public async Task<IActionResult> GetUpdatedProductInfo([FromBody] InventoryDTO inventoryDTO)
        {
            var res = await _inventoryService.UpdatedupadtedProduct(inventoryDTO);
            if (res == null)
            {
                return NotFound("An Error Occured While Updateding Product in Inventory!");
            }
            _responseDTO.IsSuccess = true;
            _responseDTO.Message = "Added Product Information in Inventor Successfully!";
            _responseDTO.Result = res;
            return Ok(_responseDTO);
        }
        //Get-Deleted-Product Information
        [HttpDelete]
        [Route("product-delete-info/{guid}")]
        public async Task<IActionResult> GetDeletedProductInfo(int guid)
        {
            var res = await _inventoryService.UpdatedDeletedProduct(guid);
            if (res == null)
            {
                return NotFound("An Error Occured While Updateding Product in Inventory!");
            }
            _responseDTO.IsSuccess = true;
            _responseDTO.Message = "Added Product Information in Inventor Successfully!";
            _responseDTO.Result = res;
            return Ok(_responseDTO);
        }
        //Get-Product-by-Guid-in-Inventory
        [HttpGet]
        [Route("get-product/{guid}")]
        public async Task<IActionResult> GetProductByGuid(int guid)
        {
            var res = await _inventoryService.GetProductbyGuid(guid);
            if(res == null)
            {
                return NotFound("Product Not Found!");
            }
            _responseDTO.IsSuccess = true;
            _responseDTO.Message = "Get Product Successfully!";
            _responseDTO.Result = res;
            return Ok(res);
        }
        [HttpPost]
        [Route("get-order-product")]
        public async Task<IActionResult> GetOrderProductByGuid([FromBody] OrderDTO orderDTO)
        {
            var res = await _inventoryService.GetOrderProductbyGuid(orderDTO);
            if (res == null)
            {
                return NotFound("Product Not Found!");
            }
            _responseDTO.IsSuccess = true;
            _responseDTO.Message = "Product Updated Successfully In Inventory!";
            _responseDTO.Result = res;
            return Ok(res);
        }
    }
}
