using ECommerce.Microservice.SharedLibrary.Logging;
using ECommerce.Microservice.SharedLibrary.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace ECommerce.Microservice.SharedLibrary.ServiceRegistration
{
    public static class SharedServiceRegistration
    {
        public static IServiceCollection AddSharedServices<TContext>(this IServiceCollection services, IConfiguration config, string loggingFileName) where TContext : DbContext
        {
            services.AddDbContext<TContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("eCommerce"));
            });

            Log.Logger = LoggingService.CreateLogger(loggingFileName!);

            return services;
        }

        public static IApplicationBuilder UseSharedPolicies(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalException>();

            //app.UseMiddleware<ServiceApiException>();

            return app;
        }
    }
}
