using HeadFirstOOAD.Helpers;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application1;

using ServicesClasses;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var appconfiguration = new Utils.Configuration.AppConfiguration();
        // Add services to the container.

        builder.Services.Configure<JWT>(appconfiguration.JWT);
        builder.Services.AddControllers();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
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
                ValidIssuer = appconfiguration.JWT.GetSection("Issuer").Value,
                ValidAudience = appconfiguration.JWT.GetSection("Audience").Value,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appconfiguration.JWT.GetSection("Key").Value))


            };
        });


        builder.Services.AddServicesOnWhichApplicationDepends();
        builder.Services.AddServicesOnWhichDataRepositoryDepend();
        builder.Services.AddMassTransit(config =>
        {



            config.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(appconfiguration.EventBusSettingsUri);
                cfg.UseHealthCheck(ctx);

                cfg.ReceiveEndpoint(Utils.Events.EventBusConstants.UserLoginQueue.Queue, c =>
                {

                });
            });
        });
        builder.Services.AddMassTransitHostedService();
        builder.Services.AddDistributedMemoryCache();
        var app = builder.Build();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        using (IServiceScope scope = app.Services.CreateScope())
        {
            var service = scope.ServiceProvider;
           await service.seedRolesAndUser();
        }
        app.Run();
    }
}