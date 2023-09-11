using DataModel;
using DataRepository;
using DataRepository.DataRepositoryEntities.DataRepositoryEntityOperationsClasses;
using DataRepository.DataRepositoryEntities.DataRepositoryOperationsInterface;
using Microsoft.Extensions.DependencyInjection;
using ServicesClasses.Interfaces;
using System;
using Validations;

namespace ServicesClasses
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServicesOnWhichServiceClassLibaryDepend(this IServiceCollection services)
        {

        
            services.AddScoped<IAuthService, AuthService>();
           

            services.AddServicesOnWhichDataRepositoryDepend().AddValidationServices(); ;
            return services;
        }

    }
}
