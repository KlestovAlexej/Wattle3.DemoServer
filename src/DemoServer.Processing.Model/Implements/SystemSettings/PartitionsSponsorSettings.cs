﻿using Newtonsoft.Json;
using ShtrihM.DemoServer.Processing.Generated.PostgreSql.Implements;
using ShtrihM.Wattle3.Common.Interfaces;
using ShtrihM.Wattle3.DomainObjects.Common;
using ShtrihM.Wattle3.DomainObjects.Interfaces;
using ShtrihM.Wattle3.Json;
using ShtrihM.Wattle3.Json.Extensions;
using ShtrihM.Wattle3.Mappers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace ShtrihM.DemoServer.Processing.Model.Implements.SystemSettings;

/// <summary>
/// Настройки создателя партиций БД.
/// </summary>
[Description("Настройки создателя партиций БД")]
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public class PartitionsSponsorSettings
{
    public class TablespaceEntry
    {
        public int Index { get; init; }
        public string TablespaceName { get; init; }
    }

    public class DomainObjectTablespaceEntry
    {
        public Guid DomainObjectType { get; init; }
        public string Comment { get; set; }
        public List<TablespaceEntry> Tablespaces { get; init; } = new();
    }

    public class TablespacesEntry
    {
        public List<TablespaceEntry> Tablespaces { get; init; } = new();
    }

    public class DomainObjectsEntry
    {
        public List<DomainObjectTablespaceEntry> DomainObjects { get; init; } = new();
    }

    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once ConvertConstructorToMemberInitializers
    public PartitionsSponsorSettings()
    {
        ActivateTimeout =
            new(
                default,
                "Интервал активации");

        TablespaceNames =
            new(
                default,
                $"Карта имён табличных пространств БД для партиций БД - Пример: {new TablespacesEntry
                {
                    Tablespaces =
                        new()
                        {
                            new()
                            {
                                Index = 0,
                                TablespaceName = "tablespace_1"
                            },
                            new()
                            {
                                Index = 1,
                                TablespaceName = "tablespace_2"
                            },
                        }
                }.ToJsonText()}");

        DomainObjectsTablespaceNames =
            new(
                default,
                $"Карта имён табличных пространств БД для партиций БД для конкретных доменных объектов - Пример: {new DomainObjectsEntry
                {
                    DomainObjects =
                        new()
                        {
                            new()
                            {
                                Comment = "Комментарий",
                                DomainObjectType = Guid.NewGuid(),
                                Tablespaces =
                                    new()
                                    {
                                        new()
                                        {
                                            Index = 0,
                                            TablespaceName = "tablespace_1"
                                        },
                                        new()
                                        {
                                            Index = 1,
                                            TablespaceName = "tablespace_2"
                                        },
                                    },
                            },
                            new()
                            {
                                Comment = "Комментарий",
                                DomainObjectType = Guid.NewGuid(),
                                Tablespaces =
                                    new()
                                    {
                                        new()
                                        {
                                            Index = 0,
                                            TablespaceName = "tablespace_1"
                                        },
                                        new()
                                        {
                                            Index = 1,
                                            TablespaceName = "tablespace_2"
                                        },
                                    },
                            },
                        }
                }.ToJsonText()}");
    }

    /// <summary>
    /// Интервал активации.
    /// </summary>
    [JsonRequired]
    public SettingValue<TimeSpan> ActivateTimeout { get; set; }

    /// <summary>
    /// Карта имён табличных пространств БД для партиций БД.
    /// </summary>
    [JsonRequired]
    public SettingValue<TablespacesEntry> TablespaceNames { get; set; }

    /// <summary>
    /// Карта имён табличных пространств БД для партиций БД для конкретных доменных объектов.
    /// </summary>
    [JsonRequired]
    public SettingValue<DomainObjectsEntry> DomainObjectsTablespaceNames { get; set; }

    /// <summary>
    /// Настройки по умолчанию.
    /// </summary>
    public static PartitionsSponsorSettings GetDefault()
    {
        var result =
            new PartitionsSponsorSettings
            {
                ActivateTimeout =
                {
                    Value = TimeSpan.FromHours(4)
                },

                TablespaceNames =
                {
                    Value = new()
                },

                DomainObjectsTablespaceNames =
                {
                    Value = new()
                },
            };

        using var mappers =
            new Mappers(
                new MappersExceptionPolicy(),
                "",
                new TimeService(),
                WellknownInfrastructureMonitors.Mappers,
                WellknownInfrastructureMonitors.GetDisplayName(WellknownInfrastructureMonitors.Mappers),
                WellknownInfrastructureMonitors.GetDisplayName(WellknownInfrastructureMonitors.Mappers));
        foreach (var manager in PartitionsSponsor.GetAllPartitionsManagers(mappers))
        {
            result.DomainObjectsTablespaceNames.Value.DomainObjects.Add(
                new()
                {
                    DomainObjectType = manager.Mapper.MapperId,
                    Comment = WellknownDomainObjectDisplayNames.DisplayNamesProvider(manager.Mapper.MapperId),
                    Tablespaces = new(),
                });
        }

        return result;
    }
}