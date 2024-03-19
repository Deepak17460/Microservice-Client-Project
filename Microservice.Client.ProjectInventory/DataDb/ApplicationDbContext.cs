using Microservice.Client.ProjectInventory.InventoryModel;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Client.ProjectInventory.DataDb
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
       public DbSet<Inventory> Inventories { get; set; }

    }
}
