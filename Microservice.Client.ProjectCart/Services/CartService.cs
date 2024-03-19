using AutoMapper;
using Microservice.Client.ProjectCart.Domain;
using Microservice.Client.ProjectCart.Models.DTOs;
using Microservice.Client.ProjectCart.Repository;
using System.Reflection.Metadata.Ecma335;

namespace Microservice.Client.ProjectCart.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        public CartService(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }
        public async Task<CartDTO> AddItemToCart(CartDTO cartDTO)
        {
            var cart = _mapper.Map<CartDTO, Cart>(cartDTO);
            return await _cartRepository.AddItemToCart(cart);
        }
        public async Task<IEnumerable<CartDTO>> GetAllCart(string UserId)
        {
            var carts = await _cartRepository.GetAllCart(UserId);
            return carts;
        }
        public async Task<bool> RemoveItemFromCart(int guid)
        {
            bool res = await _cartRepository.RemoveCart(guid);
            return res;
        }
        public async Task<IEnumerable<CartDTO>> GetAllCarts()
        {
            var res = await _cartRepository.GetAllCarts();
            return res;
        }
        public async Task<bool> RemoveItemFromCartUser(string guid)
        {
            bool res = await _cartRepository.ReomveUserCartAsync(guid);
            return res;
        }
        public async Task<CartDTO> GetUserCart(string UserId) => await _cartRepository.GetUserCartByUserId(UserId);
    }
}
