
using Microservice.Client.ProjectCart.Configuration;
using Microservice.Client.ProjectCart.UserValidate;
using Steeltoe.Discovery.Client;

namespace Microservice.Client.ProjectCart
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
            //SQL Server
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            if (connectionString != null)
            {
                builder.Services.RegisterDataContext(connectionString);
            }
            // Register HttpClient
            builder.Services.AddHttpClient();
            builder.AddAppAuthetication();
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