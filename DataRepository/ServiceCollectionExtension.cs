using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Utils.Enums;
using Utils.Enums.Classes;


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
