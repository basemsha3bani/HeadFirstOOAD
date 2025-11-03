
using Application;
using Application.Features.Users;
using Application1.Contracts;
using Application1.Validation;

using MediatR;
using Microsoft.Extensions.DependencyInjection;


using System;
using System.Reflection;
using Utils.Enums.Classes;

namespace ServicesClasses
{
    public static  class ServiceCollectionExtension
    {
        public static IServiceCollection AddServicesOnWhichApplicationDepends(this IServiceCollection services)
        {

           services.AddScoped<EnumMapper>();
            services.AddScoped<AuthService>();
            services.AddMediatR(Assembly.GetExecutingAssembly())
                     .AddValidationServices();





            return services;
        }

    }
}
