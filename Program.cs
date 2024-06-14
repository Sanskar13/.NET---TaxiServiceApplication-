using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaxiServiceAPI.Data;
using TaxiServiceAPI.Extensions;
using TaxiServiceAPI.Interfaces.IService;
using TaxiServiceAPI.Middleware;
using TaxiServiceAPI.Services;

namespace TaxiServiceAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigureJwt(builder.Configuration);
            builder.Services.RegisterServices(builder.Configuration);
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<RequestLoggingMiddleware>();

            app.MapControllers();
            app.Run();
        }
    }
}
