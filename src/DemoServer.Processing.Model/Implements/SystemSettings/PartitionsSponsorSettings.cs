﻿using Newtonsoft.Json;
using ShtrihM.DemoServer.Processing.Common;
using ShtrihM.DemoServer.Processing.Generated.PostgreSql.Implements;
using ShtrihM.Wattle3.Common.Interfaces;
using ShtrihM.Wattle3.DomainObjects.Common;
using ShtrihM.Wattle3.Json;
using ShtrihM.Wattle3.Json.Extensions;
using ShtrihM.Wattle3.Mappers;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ShtrihM.DemoServer.Processing.Model.Implements.SystemSettings;

/// <summary>
/// Настройки создателя партиций БД.
/// </summary>
[Description("Настройки создателя партиций БД")]
public class PartitionsSponsorSettings
{
    public class TablespaceEntry
    {
        public int Index { get; set; }
        public string TablespaceName { get; set; }
    }

    public class DomainObjectTablespaceEntry
    {
        public Guid DomainObjectType { get; set; }
        public string Comment { get; set; }
        public List<TablespaceEntry> Tablespaces { get; set; } = new();
    }

    public class TablespacesEntry
    {
        public List<TablespaceEntry> Tablespaces { get; set; } = new();
    }

    public class DomainObjectsEntry
    {
        public List<DomainObjectTablespaceEntry> DomainObjects { get; set; } = new();
    }

    public PartitionsSponsorSettings()
    {
        ActivateTimeout =
            new SettingValue<TimeSpan>(
                default,
                @"Интервал активации");

        TablespaceNames =
            new SettingValue<TablespacesEntry>(
                default,
                $@"Карта имён табличных пространств БД для партиций БД - Пример: {new TablespacesEntry
                {
                    Tablespaces =
                        new List<TablespaceEntry>
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
            new SettingValue<DomainObjectsEntry>(
                default,
                $@"Карта имён табличных пространств БД для партиций БД для конкретных доменных объектов - Пример: {new DomainObjectsEntry
                {
                    DomainObjects =
                        new List<DomainObjectTablespaceEntry>
                        {
                            new()
                            {
                                Comment = "Комментарий",
                                DomainObjectType = Guid.NewGuid(),
                                Tablespaces =
                                    new List<TablespaceEntry>
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
                                    new List<TablespaceEntry>
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
                    Value = new TablespacesEntry()
                },

                DomainObjectsTablespaceNames =
                {
                    Value = new DomainObjectsEntry()
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
                new DomainObjectTablespaceEntry
                {
                    DomainObjectType = manager.Mapper.MapperId,
                    Comment = WellknownDomainObjects.GetDisplayName(manager.Mapper.MapperId),
                    Tablespaces = new List<TablespaceEntry>(),
                });
        }

        return result;
    }
}