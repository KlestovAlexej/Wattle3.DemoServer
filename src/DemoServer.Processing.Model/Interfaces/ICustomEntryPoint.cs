﻿using Microsoft.Extensions.Logging;
using OpenTelemetry.Trace;
using ShtrihM.DemoServer.Processing.DataAccess.Interface;
using ShtrihM.DemoServer.Processing.Model.Implements;
using ShtrihM.DemoServer.Processing.Model.Implements.SystemSettings;
using ShtrihM.DemoServer.Processing.Model.Implements.UnitOfWorkLocks;
using ShtrihM.Wattle3.Common.Interfaces;
using ShtrihM.Wattle3.DomainObjects.Interfaces;
using System;
using ShtrihM.Wattle3.Mappers.Interfaces;
using IMapper = AutoMapper.IMapper;
using ShtrihM.Wattle3.DomainObjects.Json;

namespace ShtrihM.DemoServer.Processing.Model.Interfaces;

public interface ICustomEntryPoint : IEntryPoint
{
    // ReSharper disable once UnusedMemberInSuper.Global
    new IInfrastructureMonitorCustomEntryPoint InfrastructureMonitor { get; }

    // ReSharper disable once UnusedMemberInSuper.Global
    IServiceProvider ServiceProvider { get; }

    SystemSettingsLocal SystemSettingsLocal { get; }
    UnitOfWork CurrentUnitOfWork { get; }
    ITimeService TimeService { get; }
    WorkflowExceptionPolicy WorkflowExceptionPolicy { get; }
    MetaServerDescription ServerDescription { get; }
    SystemSettings SystemSettings { get; }
    ICustomMappers Mappers { get; }
    IExceptionPolicy ExceptionPolicy { get; }
    IPartitionsDay PartitionsDay { get; }
    IEntryPointFacade Facade { get; }
    Metrics? Metrics { get; }
    Tracer? Tracer { get; }
    ILoggerFactory LoggerFactory { get; }
    UnitOfWorkLocksHubTyped UnitOfWorkLocks { get; }
    IEntryPointContext<ICustomEntryPoint> Context { get; }
    IMapper AutoMapper { get; }
    IDemoDelayTaskProcessor DemoDelayTaskProcessor { get; }
    ISmartJsonDeserializer JsonDeserializer { get; }
}
