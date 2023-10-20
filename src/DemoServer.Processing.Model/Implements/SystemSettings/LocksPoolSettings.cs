﻿using Newtonsoft.Json;
using ShtrihM.Wattle3.Json;
using System;
using System.ComponentModel;

namespace ShtrihM.DemoServer.Processing.Model.Implements.SystemSettings;

/// <summary>
/// Настройки пулов лок-объектов.
/// </summary>
[Description("Настройки пулов лок-объектов")]
public class LocksPoolSettings
{
    public static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(3);

    public LocksPoolSettings()
    {
        UpdateDemoObject =
            new SettingValue<TimeSpan?>(
                default,
                @"Интервал ожидания получение объекта из пула лок-объектов сценария обновления объекта 'Объект'");

        UpdateDemoObjectX =
            new SettingValue<TimeSpan?>(
                default,
                @"Интервал ожидания получение объекта из пула лок-объектов сценария обновления объекта 'Объект X'");

        CreateDemoObjectX =
            new SettingValue<TimeSpan?>(
                default,
                @"Интервал ожидания получение объекта из пула лок-объектов сценария создания объекта 'Объект X'");
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