using Microsoft.AspNetCore.Identity;

namespace Microservice.Client.ProjectAuth.Domain
{
    public class User:IdentityUser
    {
        public string? Name { get; set; }
    }
}
