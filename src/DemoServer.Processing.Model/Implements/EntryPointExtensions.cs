﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShtrihM.DemoServer.Processing.DataAccess.PostgreSql.EfModels;
using ShtrihM.DemoServer.Processing.DataAccess.PostgreSql.EfModelsOptimized;
using ShtrihM.DemoServer.Processing.Model.Implements.ControllersServices;
using ShtrihM.DemoServer.Processing.Model.Interfaces;
using ShtrihM.Wattle3.DomainObjects.Interfaces;
using ShtrihM.Wattle3.Primitives;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ShtrihM.DemoServer.Processing.Model.Implements;

public static class EntryPointExtensions
{
    [SuppressMessage("ReSharper", "UnusedVariable")]
    public static void RegisterGlobals()
    {
        /* NONE */
    }

    public static WebApplication AddEntryPointServices(this WebApplication application)
    {
        /* NONE */

        return application;
    }

    public static IServiceCollection AddEntryPointServices(
        this IServiceCollection services,
        SystemSettings.SystemSettings systemSettings)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (systemSettings == null)
        {
            throw new ArgumentNullException(nameof(systemSettings));
        }

        services.AddSingleton(
            _ => (ICustomEntryPoint)ServiceProviderHolder.Instance.GetRequiredService<IEntryPoint>());

        services.AddSingleton(
            provider =>
            {
                var entryPoint = provider.GetRequiredService<ICustomEntryPoint>();
                var service = new DemoObjectControllerService(entryPoint);
                var result =
                    ControllerServiceDispatchProxy<IDemoObjectControllerService>.CreateProxy(
                        entryPoint,
                        service);

                return result;
            });

        services.AddPooledDbContextFactory<ProcessingDbContext>(
            o =>
            {
                o.UseModel(ProcessingDbContextModel.Instance);
                o.UseNpgsql();
            }
#if DEBUG
            , 1
#endif
        );

        return services;
    }
}