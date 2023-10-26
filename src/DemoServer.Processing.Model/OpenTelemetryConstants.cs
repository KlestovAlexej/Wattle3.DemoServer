﻿using ShtrihM.Wattle3.OpenTelemetry;

namespace ShtrihM.DemoServer.Processing.Model;

public static class OpenTelemetryConstants
{
    public const string AttributeMethodParameters = OpenTelemetryAttibutes.AttributePrefix + "method.parameters";
    public const string AttributeMethodResult = OpenTelemetryAttibutes.AttributePrefix + "method.result";
    public const string EventNotDefinedCommitVerifying = OpenTelemetryAttibutes.EventPrefix + "NotDefinedCommitVerifying";
    public const string EventDefinedCommitVerifying = OpenTelemetryAttibutes.EventPrefix + "DefinedCommitVerifying";
}