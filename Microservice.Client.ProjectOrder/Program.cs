
using MassTransit;
using Microservice.Client.ProjectOrder.Configuration;
using Microservice.Client.ProjectOrder.UserValidate;
using Steeltoe.Discovery.Client;

namespace Microservice.Client.ProjectOrder
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
            // Register HttpClient
            builder.Services.AddHttpClient();
            builder.AddAppAuthetication();
            var app = builder.Build();
            // Configure the HTTP request pipeline.
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