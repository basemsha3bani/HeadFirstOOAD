
using Domain.Entities.Operations.Implemenation;
using Domain.Entities.Operations.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using Utils.Enums.Classes;

namespace Domain
{

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServicesOnWhicDomainDepends(this IServiceCollection services)
        {

            services.AddScoped<EnumMapper>();
            services.AddScoped<IGuitarOperations, GuitarOperations>();
            services.AddScoped<IUsersOperations, UsersOperations>();
           


            return services;
        }
    }
}
