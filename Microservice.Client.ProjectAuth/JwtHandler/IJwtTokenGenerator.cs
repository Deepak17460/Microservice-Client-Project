using Microservice.Client.ProjectAuth.Models.DTOs;

namespace Microservice.Client.ProjectAuth.JwtHandler
{
    public interface IJwtTokenGenerator
    {
        public string GenerateToken(UserDTO applicationUser, IEnumerable<string> roles);
    }
}
