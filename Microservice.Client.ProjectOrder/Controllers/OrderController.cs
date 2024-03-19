using Microservice.Client.ProjectOrder.Models.DTOs;
using Microservice.Client.ProjectOrder.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace Microservice.Client.ProjectOrder.Controllers
{
    [Route("order")]
    //[Authorize("User")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly HttpClient _httpClient; // Add HttpClient
        private readonly ResponseDTO _responseDTO;
        public OrderController(IOrderService orderService, HttpClient httpClient)
        {
            _orderService = orderService;
            _httpClient = httpClient;
            _responseDTO = new ResponseDTO();

        }
        [HttpPost]
        [Route("item-order")]
        public async Task<IActionResult> PlacedOrder([FromBody] OrderDTO orderDTO)
        {
            try
            {
                // User Information from Token in Http Request
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    orderDTO.UserId = userIdClaim.Value;
                }
                //JWT Token
                //var jwtToken = HttpContext.Request.Headers["Authorization"].ToString();
                //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                // Handling JWT Tokens using HttpClient
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                //There will be a call to get the product information from inventory
                var response = await _httpClient.GetStringAsync($"http://172.22.0.1:8004/cart/get-cart-user");
                var cart = JsonConvert.DeserializeObject<CartDTO>(response);
                if (cart != null)
                {
                    //Getting Order From Cart Service
                    OrderDTO order=new OrderDTO()
                    {
                        UserId=cart.UserId,
                        ProductId=cart.ProductId,
                        Price = cart.Price,
                        Quantity = cart.Quantity,
                    };
                    var res = await _orderService.OrderPlaced(order);
                    if (res != null)
                    {
                        // Call UpdatedAddedProduct in Product Service
                        var orderproductDto = new OrderProductDTO
                        {
                            ProductId=cart.ProductId,
                            Quantity=cart.Quantity,
                        };
                        var inventoryJson = JsonConvert.SerializeObject(orderproductDto);
                        var content = new StringContent(inventoryJson, Encoding.UTF8, "application/json");
                        var resProduct = await _httpClient.PostAsync("http://:8002/product/order-placed-product", content);
                        var result = resProduct.EnsureSuccessStatusCode();
                        if (resProduct.IsSuccessStatusCode)
                        {
                            _responseDTO.IsSuccess = true;
                            _responseDTO.Message = "Order Placed Successfully!";
                            _responseDTO.Result = res;
                            return Ok(_responseDTO);
                        }
                        return BadRequest("Something went wrong while ordering!");
                    }
                }
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = "Order Not Placed Successfully!";
                _responseDTO.Result = "";
                return Ok(_responseDTO);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
