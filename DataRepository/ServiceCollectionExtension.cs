

using DataRepository.GateWay;
using Domain.DomainEntities;
using Domain.DomainEntities.Security;
using Microsoft.EntityFrameworkCore;
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

            services.AddDbContext<AppDbContext>();
            services.AddScoped<IContextGateway<Guitar>, ContextGateway<Guitar>>();
            services.AddScoped<IContextGateway<Users>, ContextGateway<Users>>();
           


            return services;
        }

    }
}
