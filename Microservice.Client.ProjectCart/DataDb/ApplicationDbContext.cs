using Microservice.Client.ProjectCart.Domain;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Client.ProjectCart.DataDb
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public  DbSet<Cart> Carts { get; set; }
    }
}
