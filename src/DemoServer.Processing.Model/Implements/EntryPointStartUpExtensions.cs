using ShtrihM.DemoServer.Processing.DataAccess.PostgreSql.EfModels;
using ShtrihM.DemoServer.Processing.Model.Implements.ControllersServices;
using ShtrihM.DemoServer.Processing.Model.Interfaces;
using ShtrihM.Wattle3.DomainObjects.Interfaces;
using ShtrihM.Wattle3.Primitives;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShtrihM.Wattle3.DomainObjects;
using DbContextModel = ShtrihM.DemoServer.Processing.DataAccess.PostgreSql.EfModelsOptimized.ProcessingDbContextModel;

namespace ShtrihM.DemoServer.Processing.Model.Implements;

public static class EntryPointStartUpExtensions
{
    public static void RegisterGlobals()
    {
        /* NONE */
    }

    // ReSharper disable once UnusedMethodReturnValue.Global
    public static WebApplication AddEntryPointServices(this WebApplication application)
    {
        /* NONE */

        return application;
    }

    // ReSharper disable once UnusedMethodReturnValue.Global
    public static IServiceCollection AddEntryPointServices(
        this IServiceCollection services,
        SystemSettings.SystemSettings systemSettings)
    {
        services.AddSingleton(
            _ => (ICustomEntryPoint)ServiceProviderHolder.Instance.GetRequiredService<IEntryPoint>());

        services.AddAutoMapper(typeof(AutoMapperProfile));

        services.AddSingleton(
            provider =>
            {
                var entryPoint = provider.GetRequiredService<ICustomEntryPoint>();
                var service = new Telegram(entryPoint);

                if (false == systemSettings.UseLoggingProxy.Value)
                {
                    return service;
                }

                var result =
                    LoggingDispatchProxy<ITelegram>.CreateProxy(
                        service,
                        entryPoint.LoggerFactory.CreateLogger<Telegram>(),
                        entryPoint.Tracer);

                return result;
            });

        services.AddSingleton(
            provider =>
            {
                var entryPoint = provider.GetRequiredService<ICustomEntryPoint>();
                var service = new DemoObjectControllerService(entryPoint);

                if (false == systemSettings.UseLoggingProxy.Value)
                {
                    return service;
                }

                var result = 
                    LoggingDispatchProxy<IDemoObjectControllerService>.CreateProxy(
                        service, 
                        entryPoint.LoggerFactory.CreateLogger<DemoObjectControllerService>(), 
                        entryPoint.Tracer);

                return result;
            });

        services.AddSingleton(
            provider =>
            {
                var entryPoint = provider.GetRequiredService<ICustomEntryPoint>();
                var service = new ServerControllerService(entryPoint);

                if (false == systemSettings.UseLoggingProxy.Value)
                {
                    return service;
                }

                var result =
                    LoggingDispatchProxy<IServerControllerService>.CreateProxy(
                        service,
                        entryPoint.LoggerFactory.CreateLogger<ServerControllerService>(),
                        entryPoint.Tracer);

                return result;
            });

        services.AddPooledDbContextFactory<ProcessingDbContext>(
            o =>
            {
                o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                o.UseModel(DbContextModel.Instance);
                o.UseNpgsql();

                if (o.Options.FindExtension<CoreOptionsExtension>()!.Model == null)
                {
                    throw new InvalidOperationException("������ �� ����������.");
                }
            }
#if DEBUG
            , 1
#endif
        );

        return services;
    }
}
