using AutoMapper;
using Microservice.Client.ProjectAuth.DataDB;
using Microservice.Client.ProjectAuth.Domain;
using Microservice.Client.ProjectAuth.Models.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Microservice.Client.ProjectAuth.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signinManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserRepository(ApplicationDbContext context, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _signinManager = signInManager;
            _roleManager = roleManager;

        }
        public async Task<IdentityResult> RegisterUser(User user, string password)
        {
            var res = await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(user, "User");
            return res;
        }
        public async Task<User> FindByEmail(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }
        public async Task<Boolean> CheckPasswordAsync(User userData, string password)
        {
            return await _userManager.CheckPasswordAsync(userData, password);
        }
        public async Task<IEnumerable<string>> UserRoles(User user)
        {
            var res=await _userManager.GetRolesAsync(user);
            return res;
        }
        //Admin Creation
        public async Task<IdentityResult> CreateAdmin(User user, string password)
        {
            var res = await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(user, "Admin");
            return res;
        }
    }
}
