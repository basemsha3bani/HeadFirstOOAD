using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Utils.Enums;

namespace DataRepository
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServicesOnWhichDataRepositoryDepend(this IServiceCollection services)
        {

            services.AddScoped<EnumMapper>();


            return services;
        }

    }
}
