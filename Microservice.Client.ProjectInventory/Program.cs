
using Microservice.Client.ProjectInventory.AppService;
using Microservice.Client.ProjectInventory.DataDb;
using Microservice.Client.ProjectInventory.Mapper;
using Microsoft.EntityFrameworkCore;
using Steeltoe.Discovery.Client;

namespace Microservice.Client.ProjectInventory
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add Eureka-Server.
            builder.Services.AddDiscoveryClient(builder.Configuration);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //SQL SERVER
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(o =>
            {
                o.UseSqlServer(connectionString);
            });
            //AUtoMapper
            builder.Services.AddAutoMapper(typeof(InventoryMapping));
            //Inventory Service
            builder.Services.AddScoped<IInventoryService, InventoryService>();
            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}