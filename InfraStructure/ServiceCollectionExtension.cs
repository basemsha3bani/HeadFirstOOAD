

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
            services.AddConfigurationBasedOnEnvironment(environment);

            services.AddDbContext<AppDbContext>();

            services.AddIdentity<ApplicationUser, IdentityRole>()

                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<IGuitarOperations, GuitarOperations>();
            services.AddScoped<IUsersOperations, UsersOperations>();

            services.AddScoped<IContextGateway<Guitar>, ContextGateway<Guitar>>();



            return services;
        }
        private static IServiceCollection AddConfigurationBasedOnEnvironment(this IServiceCollection services, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                services.AddSingleton<CustomConfiguration, localConfiguration>();
            }
            if (environment.IsProduction())
            {
                services.AddSingleton<CustomConfiguration, ProductionConfiguration>();
            }
            services.AddScoped<Utils.JWTConfiguration.JWTPopulator>();
            services.AddConfigurationDependantServices(environment);
            return services;
        }

        private static IServiceCollection AddConfigurationDependantServices(this IServiceCollection services, IWebHostEnvironment environment)
        {
            var svcs = services.BuildServiceProvider();
            localConfiguration localConfiguration = null;
            ProductionConfiguration productionConfiguration;
            CustomConfiguration appconfiguration;

            JWT jWT = new JWT();
            JWTPopulator jWTPopulator = svcs.GetService<JWTPopulator>();
            if (environment.IsDevelopment())
            {
                localConfiguration =(localConfiguration) svcs.GetService<CustomConfiguration>();
                jWTPopulator.PopulateJWTFromConfig(jWT, localConfiguration._jwt as Microsoft.Extensions.Configuration.IConfigurationSection);
            }
            if (environment.IsProduction())
            {
                productionConfiguration =(ProductionConfiguration) svcs.GetService<ProductionConfiguration>();
                jWTPopulator.PopulateJWTFromSecretValues(jWT, productionConfiguration._jwt as SecretClient);

            }




            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = jWT.Issuer,
                    ValidAudience = jWT.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jWT.Key))


                };
            });
            return services;

        }
         

    }
}
