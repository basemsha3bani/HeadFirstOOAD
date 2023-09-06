using DataRepository;
using DataRepository.DataRepositoryEntities.DataRepositoryEntityOperationsClasses;
using DataRepository.DataRepositoryEntities.DataRepositoryOperationsInterface;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ServicesClasses
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServicesOnWhichServiceClassLibaryDepend(this IServiceCollection services)
        {

            services.AddScoped<IGuitarOperations, GuitarOperations>();

            services.AddServicesOnWhichDataRepositoryDepend();
            return services;
        }

    }
}
