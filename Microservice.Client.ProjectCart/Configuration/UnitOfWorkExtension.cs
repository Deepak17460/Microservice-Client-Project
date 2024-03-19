using Microservice.Client.ProjectCart.DataDb;
using Microservice.Client.ProjectCart.Mapper;
using Microservice.Client.ProjectCart.Repository;
using Microservice.Client.ProjectCart.Services;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Client.ProjectCart.Configuration
{
    public static class UnitOfWorkExtension
    {
        public static IServiceCollection RegisterDataContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(o =>
            {
                o.UseSqlServer(connectionString);
            });
            services.AddAutoMapper(typeof(CartMapping));
           services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("User", policy => policy.RequireRole("User"));
            });
            return services;
        }
    }
}
