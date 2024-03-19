using Microservice.Client.ProjectCart.Domain;
using Microservice.Client.ProjectCart.Models.DTOs;
using System;

namespace Microservice.Client.ProjectCart.Repository
{
    public interface ICartRepository
    {
        public Task<CartDTO> AddItemToCart(Cart cart);
        public Task<IEnumerable<CartDTO>> GetAllCart(string UserId);

        public Task<bool> RemoveCart(int guid);

        public Task<IEnumerable<CartDTO>> GetAllCarts();

        public Task<bool> ReomveUserCartAsync(string guid);

        public Task<CartDTO> GetUserCartByUserId(string UserId);
    }
}
