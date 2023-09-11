using DataRepository.DataRepositoryEntities.DataRepositoryEntityOperationsClasses;
using DataRepository.DataRepositoryEntities.DataRepositoryOperationsInterface;
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
            services.AddScoped<IGuitarOperations, GuitarOperations>();
            services.AddScoped<IUsersOperations, UsersOperations>();


            return services;
        }

    }
}
