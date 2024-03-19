using AutoMapper;
using Microservice.Client.ProjectCart.DataDb;
using Microservice.Client.ProjectCart.Domain;
using Microservice.Client.ProjectCart.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Client.ProjectCart.Repository
{
    public class CartRepository:ICartRepository
    { 

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CartRepository(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CartDTO> AddItemToCart(Cart cart)
        {
            Cart _cart=new Cart()
            {
                Price = cart.Price,
                Quantity = cart.Quantity,
                 UserId = cart.UserId,
                ProductId = cart.ProductId,
            };
            await _context.Carts.AddAsync(_cart);
            await _context.SaveChangesAsync();
            return _mapper.Map<Cart,CartDTO>(_cart);
        }
        public async Task<IEnumerable<CartDTO>> GetAllCart(string UserId)
        {
            var carts = await _context.Carts
                .Where(c => c.UserId == UserId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<Cart>, IEnumerable<CartDTO>>(carts);
        }
        public async Task<bool> RemoveCart(int guid)
        {
            var  res=await _context.Carts.FirstOrDefaultAsync(c=>c.Id == guid);
            if (res != null)
            {
                _context.Carts.Remove(res);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public async Task<IEnumerable<CartDTO>> GetAllCarts()
        {
            var carts = await _context.Carts.ToListAsync();
            return _mapper.Map<IEnumerable<Cart>, IEnumerable<CartDTO>>(carts);
        }
        public async Task<bool> ReomveUserCartAsync(string guid)
        {
            var res = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == guid);
            if(res != null)
            {
                _context.Carts.Remove(res);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public async Task<CartDTO> GetUserCartByUserId(string userId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
            return _mapper.Map<Cart,CartDTO>(cart);
            //=>await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}
