using Microservice.Client.ProjectCart.Models.DTOs;
using Microservice.Client.ProjectCart.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Microservice.Client.ProjectCart.Controllers
{
    [Route("cart")]
   // [Authorize("User")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly HttpClient _httpClient; // Add HttpClient
        private readonly ResponseDTO _responseDTO;
        public CartController(ICartService cartService, HttpClient httpClient)
        {
            _cartService = cartService;
            _httpClient = httpClient;
            _responseDTO = new ResponseDTO();

        }
        [HttpPost]
        [Route("add-cart")]
        public async Task<IActionResult> AddToCart([FromBody] CartDTO cartDTO)
        {
            try
            {
                // User Information from Token in Http Request
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    cartDTO.UserId = userIdClaim.Value;
                }
                //There will be a call to get the product information from inventory
                var response = await _httpClient.GetStringAsync($"http://172.22.0.1:8003/inventory/get-product/{cartDTO.ProductId}");
                var product = JsonConvert.DeserializeObject<InventoryDTO>(response);
                if (product != null && cartDTO.Quantity <= product.Quantity)
                {
                    cartDTO.Price = product.Price;
                    var res = await _cartService.AddItemToCart(cartDTO);
                    if (res == null) {
                        return NotFound("Something Went Wrong While Adding Item to Cart!");
                    }
                    _responseDTO.IsSuccess = true;
                    _responseDTO.Message = "Item Added To Cart SuccessFully!";
                    _responseDTO.Result = res;
                    return Ok(_responseDTO);
                }
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = "You Can Not Add Item To the Cart!";
                _responseDTO.Result = "";
                return NotFound(_responseDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("get-cart-all-user")]
        public async Task<IActionResult> GetAllCartByUserName()
        {
            try
            {
                // User Information from Token in Http Request
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
                var UserId = userIdClaim.Value;
                var res = await _cartService.GetAllCart(UserId);
                if (res == null) { return NotFound("Something Went Wrong While Fetching All Items in Cart!"); }
                _responseDTO.IsSuccess = true;
                _responseDTO.Message = "Get All Items In Cart SuccessFully!";
                _responseDTO.Result = res;
                return Ok(_responseDTO);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("get-cart-user")]
        public async Task<IActionResult> GetCartByUser()
        {
            try
            {
                // User Information from Token in Http Request
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
                var UserId = userIdClaim.Value;
                var res = await _cartService.GetUserCart(UserId);
                if (res == null) { return NotFound("Something Went Wrong While Fetching All Items in Cart!"); }
                _responseDTO.IsSuccess = true;
                _responseDTO.Message = "Get All Items In Cart SuccessFully!";
                _responseDTO.Result = res;
                return Ok(res);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("get-all-carts")]
        [Authorize("Admin")]

        public async Task<IActionResult> GetAllCarts()
        {
            try
            {
                var res=await _cartService.GetAllCarts();
                if (res == null) { return NotFound("Something Went Wrong While Fetching All Items in Cart!"); }
                _responseDTO.IsSuccess = true;
                _responseDTO.Message = "Get All Items In Carts!";
                _responseDTO.Result = res;
                return Ok(_responseDTO);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete]
        [Route("delete-cart/{guid}")]
        public async Task<IActionResult> DeleteItemFromCartByGuid(int guid)
        {
            try
            {
                var res = await _cartService.RemoveItemFromCart(guid);
                if (!res) { return NotFound("Something Went Wrong While Deleting Item in Cart!"); }
                _responseDTO.IsSuccess = true;
                _responseDTO.Message = "Deleted Item Successfully!";
                _responseDTO.Result = res;
                return Ok(_responseDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete]
        [Route("delete-cart-user/{guid}")]
        public async Task<IActionResult> DeleteItemFromCartByUserGuid(string guid)
        {
            try
            {
                var res = await _cartService.RemoveItemFromCartUser(guid);
                if (!res) { return NotFound("Something Went Wrong While Deleting Item in Cart!"); }
                _responseDTO.IsSuccess = true;
                _responseDTO.Message = "Deleted Item Successfully!";
                _responseDTO.Result = res;
                return Ok(_responseDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
