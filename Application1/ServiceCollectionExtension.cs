
using Application;
using Application.Features.Users;
using Application1.Contracts;
using Application1.Validation;
using Azure.Security.KeyVault.Secrets;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System;
using System.Reflection;
using System.Text;
using Utils;
using Utils.Configuration;
using Utils.Enums.Classes;
using Utils.JWTConfiguration;

namespace ServicesClasses
{
    public static  class ServiceCollectionExtension
    {
        public static IServiceCollection AddServicesOnWhichApplicationDepends(this IServiceCollection services)
        {

           services.AddScoped<EnumMapper>();
            services.AddScoped<AuthService>()
                    .AddScoped<RegistrationService>();
            services.AddMediatR(Assembly.GetExecutingAssembly())
                     .AddValidationServices();





            return services;
        }
        public static IServiceCollection AddConfigurationBasedOnEnvironment(this IServiceCollection services, IWebHostEnvironment environment)
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
                localConfiguration = (localConfiguration)svcs.GetService<CustomConfiguration>();
                jWTPopulator.PopulateJWTFromConfig(jWT, localConfiguration._jwt as Microsoft.Extensions.Configuration.IConfigurationSection);
                services.AddSingleton(ConnectionMultiplexer.Connect(localConfiguration.CacheConnectionString));
            }
            if (environment.IsProduction())
            {
                productionConfiguration = (ProductionConfiguration)svcs.GetService<ProductionConfiguration>();
                jWTPopulator.PopulateJWTFromSecretValues(jWT, productionConfiguration._jwt as SecretClient);
                services.AddSingleton(ConnectionMultiplexer.Connect(productionConfiguration.CacheConnectionString));

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
