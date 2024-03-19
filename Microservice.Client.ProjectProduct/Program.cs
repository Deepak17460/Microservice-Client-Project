using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microservice.Client.ProjectProduct.Configuration;
using Microservice.Client.ProjectProduct.UserValidate;
using Microsoft.Extensions.FileProviders;
using Steeltoe.Discovery.Client;
using MassTransit;

namespace Microservice.Client.ProjectProduct
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Add Eureka-Server.
            builder.Services.AddDiscoveryClient(builder.Configuration);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            if (connectionString != null)
            {
                builder.Services.RegisterDataContext(connectionString).GetAwaiter();
            }
            // Register HttpClient
            builder.Services.AddHttpClient();
            builder.AddAppAuthetication();
            //RabbitMQ
            builder.Services.AddMassTransit(busConfigurator => {
                busConfigurator.SetKebabCaseEndpointNameFormatter();
                busConfigurator.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(new Uri(builder.Configuration["MessageBroker:Host"]!), h =>
                    {
                        h.Username(builder.Configuration["MessageBroker:Username"]);
                        h.Password(builder.Configuration["MessageBroker:Password"]);
                    });
                    configurator.ConfigureEndpoints(context);
                });

            });
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            // Add the UseStaticFiles middleware before MapControllers
            //app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(builder.Environment.ContentRootPath, "wwwroot", "ProductImages")),
                RequestPath = "/ProductImages"
            });
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
