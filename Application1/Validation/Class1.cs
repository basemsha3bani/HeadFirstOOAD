using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application1.Validation
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
