using Microservice.Client.ProjectProduct.Domain;
using Microservice.Client.ProjectProduct.Models.DTOs;
using Microservice.Client.ProjectProduct.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace Microservice.Client.ProjectProduct.Controllers
{
    [Route("product")]
    //[Authorize("Admin")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ResponseDTO _responseDTO;
        //private static readonly HttpClient client = new();
        private readonly HttpClient _httpClient; // Add HttpClient
        public ProductController(IProductService productService, IWebHostEnvironment webHostEnvironment, HttpClient httpClient)
        {
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
            _responseDTO = new ResponseDTO();
            _httpClient = httpClient;
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("add-product")]
       // [Authorize("Admin")]
        public async Task<IActionResult> AddProduct([FromForm] ProductDTO productDTO)
        {
            try
            {
                var productDto = await _productService.CreateProduct(productDTO);
                if (productDTO.Image != null)
                {
                    var fileName = productDto.Id + Path.GetExtension(productDTO.Image.FileName);
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "ProductImages");
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    // Save the uploaded file
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await productDTO.Image.CopyToAsync(fileStream);
                    }

                    // Update productDTO with image details
                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                    productDTO.ImageUrl = baseUrl + "/ProductImages/" + fileName;
                    productDTO.ImageLocalPath = filePath;
                }
                else
                {
                    productDTO.ImageUrl = "https://placehold.co/600x400";
                }

                // Update the product with image details
                productDTO.Id=productDto.Id;
                var res = await _productService.UpdateProduct(productDTO);

                // Call UpdatedAddedProduct in Inventory Service
                var inventoryDto = new InventoryDTO
                {
                    ProductId = productDto.Id,
                    Quantity = productDTO.Quantity,
                    Price=productDto.Price,
                };
                var inventoryJson = JsonConvert.SerializeObject(inventoryDto);
                var content = new StringContent(inventoryJson, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("http://172.22.0.1:8003/inventory/product-added-info", content);
                var result= response.EnsureSuccessStatusCode();
                _responseDTO.IsSuccess = true;
                _responseDTO.Message = "Product is created successfully!";
                _responseDTO.Result = res;

                return Ok(_responseDTO);
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = "Error creating product: " + ex.Message;
                return StatusCode(500, _responseDTO);
            }
        }
        [HttpPut]
        [Route("update-product/{productId}")]
       // [Authorize("Admin")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromForm] ProductDTO updatedProductDTO)
        {
            try
            {
                var existingProduct = await _productService.GetProductByIdAsync(productId);
                if (existingProduct == null)
                {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = "Product Not Found!";
                    return NotFound(_responseDTO);
                }
                if (updatedProductDTO.Image != null)
                {
                    var fileName = existingProduct.Id + Path.GetExtension(updatedProductDTO.Image.FileName);
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "ProductImages");
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await updatedProductDTO.Image.CopyToAsync(fileStream);
                    }
                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                    existingProduct.ImageUrl = baseUrl + "/ProductImages/" + fileName;
                    existingProduct.ImageLocalPath = filePath;
                }
                var updateResult = await _productService.UpdateProductAsync(existingProduct,productId,updatedProductDTO);

                // Call GetUpdatedProductInfo in Inventory Service
                var inventoryDto = new InventoryDTO
                {
                    ProductId = updateResult.Id,
                    Quantity = updateResult.Quantity,
                    Price= updateResult.Price,
                };
                var inventoryJson = JsonConvert.SerializeObject(inventoryDto);
                var content = new StringContent(inventoryJson, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync("http://172.22.0.1:8003/inventory/product-updated-info", content);
                var result = response.EnsureSuccessStatusCode();
                _responseDTO.IsSuccess = true;
                _responseDTO.Message = "Product updated successfully!";
                _responseDTO.Result = updateResult;

                return Ok(_responseDTO);
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = "Error updating product: " + ex.Message;
                return StatusCode(500, _responseDTO);
            }
        }

        [HttpDelete]
        [Route("delete-product/{guid}")]
       // [Authorize("Admin")]
        public async Task<IActionResult> DeleteProduct(int guid)
        {
            try
            {
                bool res = await _productService.RemoveProductAsync(guid);
                if (!res)
                {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = "Something went wrong while deleting product!";
                    _responseDTO.Result = null;
                    return Ok(_responseDTO);
                }

                // Call GetDeletedProductInfo in Inventory Service
                var response = await _httpClient.DeleteAsync($"http://172.22.0.1:8003/inventory/product-delete-info/{guid}");
                var result= response.EnsureSuccessStatusCode();
                _responseDTO.IsSuccess = true;
                _responseDTO.Message = "Product Deleted Successfully!";
                _responseDTO.Result = res;
                return Ok(_responseDTO);
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = $"An error occurred: {ex}";
                _responseDTO.Result = null;
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
        [HttpGet]
        [Route("get-all-products")]
        //[Authorize("Admin")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();

                _responseDTO.IsSuccess = true;
                _responseDTO.Message = "Products retrieved successfully!";
                _responseDTO.Result = products;

                return Ok(_responseDTO);
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = "Error retrieving products: " + ex.Message;
                return StatusCode(500, _responseDTO);
            }
        }
        [HttpGet]
        [Route("get-product/{guid}")]
       // [Authorize("Admin")]
        public async Task<IActionResult> GetProductByGiud(int guid)
        {
            try
            {
                var Product = await _productService.GetProductByIdAsync(guid);
                if (Product == null)
                {
                    return NotFound("Product Not Found!");
                }
                _responseDTO.IsSuccess = true;
                _responseDTO.Message = "Retrieved Products Successfully!";
                _responseDTO.Result = Product;

                return Ok(_responseDTO);
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = "Error retrieving product" + ex.Message;
                return StatusCode(500, _responseDTO);
            }
        }
        [HttpPost]
        [Route("order-placed-product")]
        public async Task<IActionResult> GetPlacedProduct([FromBody] OrderDTO orderDTO)
        {
            var res = await _productService.GetPlacedOrder(orderDTO);
            if (res != null)
            {
                // Call GetUpdatedProductInfo in Inventory Service
                var orderDto = new OrderDTO
                {
                    ProductId = orderDTO.ProductId,
                    Quantity = res.Quantity,
                    
                };
                var inventoryJson = JsonConvert.SerializeObject(orderDto);
                var content = new StringContent(inventoryJson, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("http://172.22.0.1:8003/inventory/get-order-product", content);
                var result = response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    _responseDTO.IsSuccess = true;
                    _responseDTO.Message = "Product Updated After Order!";
                    _responseDTO.Result = res;
                }
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = "Product Not Updated After Order!";
                _responseDTO.Result ="";
            }
            _responseDTO.IsSuccess = false;
            _responseDTO.Message = "Product Not Updated After Order!";
            _responseDTO.Result =" ";
            return Ok(_responseDTO);    
        }

    }
}
