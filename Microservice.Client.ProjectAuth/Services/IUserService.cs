using Microservice.Client.ProjectAuth.Domain;
using Microservice.Client.ProjectAuth.Models.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Microservice.Client.ProjectAuth.Services
{
    public interface IUserService
    {
        public Task<IdentityResult> RegisterUser(UserSignUpDTO userSignUpDTO);
        public Task<UserDTO> FindByEmailAsync(string email);

        public Task<Boolean> CheckPasswordAsync(UserDTO user, string password);

        public Task<IEnumerable<string>> UserRoles(UserDTO user);
        public Task<IdentityResult> CreateAdmin(UserSignUpDTO userSignUpDTO);
    }
}
