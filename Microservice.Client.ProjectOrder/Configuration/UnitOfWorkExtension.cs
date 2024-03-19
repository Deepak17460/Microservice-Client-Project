using Microservice.Client.ProjectOrder.DataDb;
using Microservice.Client.ProjectOrder.Mapper;
using Microservice.Client.ProjectOrder.Repository;
using Microservice.Client.ProjectOrder.Services;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Client.ProjectOrder.Configuration
{
    public static class UnitOfWorkExtension
    {
        public static async Task<IServiceCollection> RegisterDataContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(o =>
            {
                o.UseSqlServer(connectionString);
            });
            services.AddAutoMapper(typeof(OrderMapping));
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("User", policy => policy.RequireRole("User"));
            });
            return services;
        }
    }
}
