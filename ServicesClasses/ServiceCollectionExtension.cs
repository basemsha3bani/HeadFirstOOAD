
using Application;
using DataRepository;

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
            services.AddScoped<IGuitarServices, GuitarService>();


            services.AddServicesOnWhichApplicationDepends().AddServicesOnWhichDataRepositoryDepend().AddValidationServices(); ;
            return services;
        }

    }
}
