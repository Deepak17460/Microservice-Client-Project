using Microservice.Client.ProjectProduct.DataDb;
using Microservice.Client.ProjectProduct.Mapper;
using Microservice.Client.ProjectProduct.Repository;
using Microservice.Client.ProjectProduct.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Client.ProjectProduct.Configuration
{
    public static class UnitOfWorkExtension
    {
        public static async Task<IServiceCollection> RegisterDataContext(this IServiceCollection services,string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(o =>
            {
                o.UseSqlServer(connectionString);
            });
            services.AddAutoMapper(typeof(ProductMapping));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("User", policy => policy.RequireRole("User"));
            });
            return services;
        }
    }
}
