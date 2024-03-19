using Microservice.Client.ProjectCart.Models.DTOs;
using System;

namespace Microservice.Client.ProjectCart.Services
{
    public interface ICartService
    {
        public Task<CartDTO> AddItemToCart(CartDTO cartDTO);
        public Task<IEnumerable<CartDTO>> GetAllCart(string UserId);
        public Task<bool> RemoveItemFromCart(int guid);

        public Task<IEnumerable<CartDTO>> GetAllCarts();

        public Task<bool> RemoveItemFromCartUser(string guid);

        public Task<CartDTO> GetUserCart(string UserId);
    }
}
