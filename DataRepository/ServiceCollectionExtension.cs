

using DataRepository;
using DataRepository.GateWay;
using Domain.Entities.Schema.dbo;
using Domain.Entities.Schema.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace ServicesClasses
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServicesOnWhichDataRepositoryDepend(this IServiceCollection services)
        {

            services.AddScoped<AppDbContext>();
            services.AddScoped<IContextGateway<Guitar>, ContextGateway<Guitar>>();
            services.AddScoped<IContextGateway<Users>, ContextGateway<Users>>();
            
            return services;
        }

    }
}
