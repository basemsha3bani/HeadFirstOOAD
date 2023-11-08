
using Application;
using Application.EntityOperationsInterface;
using DataRepository;
using Domain.DataRepositoryEntities.DataRepositoryEntityOperationsClasses;
using Microsoft.Extensions.DependencyInjection;

using System;
using Utils.Enums.Classes;

namespace ServicesClasses
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServicesOnWhichApplicationDepends(this IServiceCollection services)
        {

            services.AddScoped<EnumMapper>();
            services.AddScoped<IGuitarOperations, GuitarOperations>();
            services.AddScoped<IUsersOperations, UsersOperations>();
           

            
            return services;
        }

    }
}
