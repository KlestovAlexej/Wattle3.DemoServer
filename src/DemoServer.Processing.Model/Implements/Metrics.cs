﻿using System;
using System.Diagnostics.Metrics;

namespace ShtrihM.DemoServer.Processing.Model.Implements;

public class Metrics
{
    public Metrics(Meter meter)
    {
        Meter = meter ?? throw new ArgumentNullException(nameof(meter));

        RequestsIncoming = Meter.CreateCounter<int>(
            name: "shtrihm_DemoServer_processing_requests_incoming",
            unit: "{requests}",
            description: "Количество входящих запросов");

        RequestsIncomingSuccessful = Meter.CreateCounter<int>(
            name: "shtrihm_DemoServer_processing_requests_incoming_successful",
            unit: "{requests}",
            description: "Количество входящих запросов - успешных");

        ExceptionsWorkflow = Meter.CreateCounter<int>(
            name: "shtrihm_DemoServer_processing_exceptions_workflow",
            unit: "{exceptions}",
            description: "Количество штатных исключений");

        ExceptionsUnexpected = Meter.CreateCounter<int>(
            name: "shtrihm_DemoServer_processing_exceptions_unexpected",
            unit: "{exceptions}",
            description: "Количество неожиданных исключений");

        RequestsHealthCheck = Meter.CreateCounter<int>(
            name: "shtrihm_DemoServer_processing_requests_healthcheck",
            unit: "{requests}",
            description: "Количество входящих запросов проверки работоспособности");
    }

    public readonly Meter Meter;

    /// <summary>
    /// Количество входящих запросов.
    /// </summary>
    public readonly Counter<int> RequestsIncoming;

    /// <summary>
    /// Количество входящих запросов - успешных.
    /// </summary>
    public readonly Counter<int> RequestsIncomingSuccessful;

    /// <summary>
    /// Количество штатных исключений.
    /// </summary>
    public readonly Counter<int> ExceptionsWorkflow;

    /// <summary>
    /// Количество неожиданных исключений.
    /// </summary>
    public readonly Counter<int> ExceptionsUnexpected;

    /// <summary>
    /// Количество входящих запросов проверки работоспособности.
    /// </summary>
    public readonly Counter<int> RequestsHealthCheck;
}