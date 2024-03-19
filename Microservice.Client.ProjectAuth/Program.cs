using Microservice.Client.ProjectAuth.DataDB;
using Microservice.Client.ProjectAuth.Mapper;
using Microservice.Client.ProjectAuth.Repository;
using Microservice.Client.ProjectAuth.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microservice.Client.ProjectAuth.Domain;
using Microsoft.AspNetCore.Identity;
using Microservice.Client.ProjectAuth.Configuration;
using Microsoft.AspNetCore.Http.Features;
using Microservice.Client.ProjectAuth.JwtHandler;
using Microservice.Client.ProjectCart.UserValidate;
using Steeltoe.Discovery.Client;

namespace Microservice.Client.ProjectAuth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add Eureka-Server.
            builder.Services.AddDiscoveryClient(builder.Configuration);
            // Add services to the container.
            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //SQl server
            //Services to the Container
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            if (connectionString != null)
            {
                builder.Services.RegisterDataContext(connectionString).GetAwaiter();
            }
            builder.Services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
            // Register HttpClient
            builder.Services.AddHttpClient();
            builder.AddAppAuthetication();
            //JWT
            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
            //builder.Services.AddAutoMapper(typeof(AuthMapper));
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization(); 
            app.MapControllers();
            app.Run();
        }
    }
}
