using ERP.Business.Services;
using ERP.Core.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ERP.Business
{
    public static class Dependency
    {
        public static IServiceCollection AddBuisnessDI(this IServiceCollection services) 
        {
            // Register JWT Service
            services.AddScoped<IJwtService, JwtService>();
            
            // Register Auth Service
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }

    }
}
