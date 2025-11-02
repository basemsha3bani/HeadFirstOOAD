

using Application.EntityOperationsInterface;
using Application1.Contracts;
using DataRepository;
using DataRepository.GateWay;
using Domain.DataRepositoryEntities.DataRepositoryEntityOperationsClasses;
using Domain.Entities.Schema.dbo;
using Domain.Entities.Schema.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace ServicesClasses
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServicesOnWhichDataRepositoryDepend(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();
            services.AddIdentity<Users,IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<IGuitarOperations,GuitarOperations>();
            services.AddScoped<IUsersOperations, UsersOperations>();
            
            services.AddScoped<IContextGateway<Guitar>, ContextGateway<Guitar>>();
            services.AddScoped<IContextGateway<Users>, ContextGateway<Users>>();
           
            
            return services;
        }

    }
}
