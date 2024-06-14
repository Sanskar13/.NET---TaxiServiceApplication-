using Microsoft.EntityFrameworkCore;
using TaxiServiceAPI.Data;
using TaxiServiceAPI.Interfaces.IService;
using TaxiServiceAPI.Middleware;
using TaxiServiceAPI.Services;

namespace TaxiServiceAPI.Extensions
{
    public static class ServiceRegistrationExtension
    {
        /// <summary>
        /// Registers services in the dependency injection container.
        /// </summary>
        /// <param name="services">The service collection to register services with.</param>
        /// <param name="configuration">The configuration to use for registering services.</param>
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MySQLDBContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddScoped<IRideService, RideService>();
            services.AddScoped<IDriverService, DriverService>();
            services.AddScoped<IRiderService, RiderService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<RequestLoggingMiddleware>();
        }
    }
}
