using Microservice.Client.ProjectOrder.Domain;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Client.ProjectOrder.DataDb
{
    public class ApplicationDbContext:DbContext

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
    }
}
