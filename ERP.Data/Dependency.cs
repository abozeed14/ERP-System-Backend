using ERP.Data.Data;
using ERP.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace ERP.Data
{
    public static class Dependency
    {
        public static IServiceCollection AddDataDI(this IServiceCollection services , IConfiguration configuration) 
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"));
            });

           
            return services;
        }
    }
}
