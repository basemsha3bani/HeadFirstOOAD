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
using Utils;
using Utils.Configuration;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
       
       
        // Add services to the container.

        
        builder.Services.AddControllers();




        builder.Services.AddServicesOnWhichApplicationDepends()
                .AddConfigurationBasedOnEnvironment(builder.Environment);
         builder.Services.AddServicesOnWhichDataRepositoryDepend(builder.Environment);
        var svcs = builder.Services.BuildServiceProvider();
        ProductionConfiguration productionConfiguration = builder.Environment.IsProduction()?(ProductionConfiguration) svcs.GetRequiredService<CustomConfiguration>():null;
        localConfiguration localConfiguration = builder.Environment.IsDevelopment() ?(localConfiguration) svcs.GetService<CustomConfiguration>():null;
        builder.Services.AddMassTransit(config =>
        {



            config.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(productionConfiguration != null? productionConfiguration._EventBusSettingsUri.ToString():localConfiguration._EventBusSettingsUri.ToString());
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