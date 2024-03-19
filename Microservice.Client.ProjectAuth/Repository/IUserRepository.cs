using Microservice.Client.ProjectAuth.Domain;
using Microservice.Client.ProjectAuth.Models.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Microservice.Client.ProjectAuth.Repository
{
    public interface IUserRepository
    {
        public Task<IdentityResult> RegisterUser(User user, string Password);
        public Task<User> FindByEmail(string email);

        public Task<Boolean> CheckPasswordAsync(User userData, string password);
       public  Task<IEnumerable<string>> UserRoles(User user);

        public Task<IdentityResult> CreateAdmin(User userData, string password);
    }
}
