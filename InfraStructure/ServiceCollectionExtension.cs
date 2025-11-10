

using Application.EntityOperationsInterface;
using Application1.Contracts;
using Azure.Security.KeyVault.Secrets;
using DataRepository;
using DataRepository.GateWay;
using Domain.DataRepositoryEntities.DataRepositoryEntityOperationsClasses;
using Domain.Entities.Schema.dbo;
using Domain.Entities.Schema.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Emit;
using System.Security.Cryptography.Xml;
using System.Text;
using Utils;
using Utils.Configuration;

using Utils.JWTConfiguration;


namespace ServicesClasses
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServicesOnWhichDataRepositoryDepend(this IServiceCollection services, IWebHostEnvironment environment)
        {
            

            services.AddDbContext<AppDbContext>();

            services.AddIdentity<ApplicationUser, IdentityRole>()

                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<IGuitarOperations, GuitarOperations>();
            services.AddScoped<IUsersOperations, UsersOperations>();

            services.AddScoped<IContextGateway<Guitar>, ContextGateway<Guitar>>();



            return services;
        }
       
         

    }
}
