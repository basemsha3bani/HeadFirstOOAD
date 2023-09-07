using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Validations
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddValidationServices(this IServiceCollection services)
        {
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GuitarValidator>());
            return services;
        }
    }
}
