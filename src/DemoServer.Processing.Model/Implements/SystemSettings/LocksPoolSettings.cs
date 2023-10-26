﻿using Newtonsoft.Json;
using ShtrihM.DemoServer.Processing.Common;
using ShtrihM.Wattle3.DomainObjects.Interfaces;
using ShtrihM.Wattle3.Json;
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace ShtrihM.DemoServer.Processing.Model.Implements.SystemSettings;

/// <summary>
/// Настройки пулов лок-объектов.
/// </summary>
[Description("Настройки пулов лок-объектов")]
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public class LocksPoolSettings
{
    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(3);

    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once ConvertConstructorToMemberInitializers
    public LocksPoolSettings()
    {
        UpdateDemoObject =
            new(
                default,
                $"Интервал ожидания получение объекта из пула лок-объектов сценария обновления объекта '{WellknownDomainObjectDisplayNames.DisplayNamesProvider(WellknownDomainObjects.DemoObject)}'");

        UpdateDemoObjectX =
            new(
                default,
                $"Интервал ожидания получение объекта из пула лок-объектов сценария обновления объекта '{WellknownDomainObjectDisplayNames.DisplayNamesProvider(WellknownDomainObjects.DemoObjectX)}'");

        CreateDemoObjectX =
            new(
                default,
                $"Интервал ожидания получение объекта из пула лок-объектов сценария создания объекта '{WellknownDomainObjectDisplayNames.DisplayNamesProvider(WellknownDomainObjects.DemoObjectX)}'");
    }

    /// <summary>
    /// Интервал ожидания получение объекта из пула лок-объектов сценария обновления объекта 'Объект'.
    /// </summary>
    [JsonRequired]
    public SettingValue<TimeSpan?> UpdateDemoObject { get; set; }

    /// <summary>
    /// Интервал ожидания получение объекта из пула лок-объектов сценария обновления объекта 'Объект X'.
    /// </summary>
    [JsonRequired]
    public SettingValue<TimeSpan?> UpdateDemoObjectX { get; set; }

    /// <summary>
    /// Интервал ожидания получение объекта из пула лок-объектов сценария создания объекта 'Объект X'.
    /// </summary>
    [JsonRequired]
    public SettingValue<TimeSpan?> CreateDemoObjectX { get; set; }

    /// <summary>
    /// Настройки по умолчанию.
    /// </summary>
    public static LocksPoolSettings GetDefault()
    {
        return new()
        {
            UpdateDemoObject =
            {
                Value = DefaultTimeout
            },
            UpdateDemoObjectX =
            {
                Value = DefaultTimeout
            },
            CreateDemoObjectX = 
            {
                Value = DefaultTimeout
            },
        };
    }
}