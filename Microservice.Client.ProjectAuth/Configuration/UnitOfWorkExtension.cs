using Microservice.Client.ProjectAuth.DataDB;
using Microservice.Client.ProjectAuth.DataDB.Seed;
using Microservice.Client.ProjectAuth.Domain;
using Microservice.Client.ProjectAuth.JwtHandler;
using Microservice.Client.ProjectAuth.Mapper;
using Microservice.Client.ProjectAuth.Repository;
using Microservice.Client.ProjectAuth.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Client.ProjectAuth.Configuration
{
    public static class UnitOfWorkExtension
    {
        public static async Task<IServiceCollection> RegisterDataContext(this IServiceCollection services,string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(o =>
            {
                o.UseSqlServer(connectionString);
            });
            services.AddAutoMapper(typeof(AuthMapper));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddIdentity<User, IdentityRole>()
           .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders();
            services.AddScoped<UserManager<User>>();
            services.AddScoped<SignInManager<User>>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("User", policy => policy.RequireRole("User"));
            });
           await AdminDataSeed.Seed(services.BuildServiceProvider());
            return services;
        }
    }
}
