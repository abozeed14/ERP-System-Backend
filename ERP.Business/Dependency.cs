using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Business
{
    public static class Dependency
    {
        public static IServiceCollection AddBuisnessDI(this IServiceCollection services) 
        {

            return services;
        }

    }
}
