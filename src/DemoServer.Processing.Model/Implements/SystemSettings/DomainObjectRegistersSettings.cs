using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using ShtrihM.DemoServer.Processing.Common;
using ShtrihM.DemoServer.Processing.DataAccess.Interface;
using ShtrihM.Wattle3.Caching;
using ShtrihM.Wattle3.DomainObjects.Interfaces;
using ShtrihM.Wattle3.Json;

namespace ShtrihM.DemoServer.Processing.Model.Implements.SystemSettings;

/// <summary>
/// ��������� �������� �������� ��������.
/// </summary>
[Description("��������� �������� �������� ��������")]
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public class DomainObjectRegistersSettings
{
    private static readonly int MemoryCacheMaxItems = 10_000;

    // ReSharper disable once ConvertConstructorToMemberInitializers
    // ReSharper disable once MemberCanBePrivate.Global
    public DomainObjectRegistersSettings()
    {
        MemoryCacheDemoObjectX =
            new SettingValue<MemoryCacheSettings>(
                default!,
                $"��������� ����������� ������� �������� �������� - {WellknownDomainObjectDisplayNames.DisplayNamesProvider(WellknownDomainObjects.DemoObjectX)}");

        UseIdentitiesServices =
            new SettingValue<bool>(
                default,
                "������������ ������� �������� ��������");
    }

    /// <summary>
    /// ��������� ����������� ������� �������� �������� - ������ X.
    /// </summary>
    [Description("��������� ����������� ������� �������� �������� - ������ X")]
    [JsonRequired]
    public SettingValue<MemoryCacheSettings> MemoryCacheDemoObjectX { get; set; }

    /// <summary>
    /// ������������ ������� �������� ��������.
    /// </summary>
    [JsonRequired]
    public SettingValue<bool> UseIdentitiesServices { get; set; }

    /// <summary>
    /// ��������� �� ���������.
    /// </summary>
    public static DomainObjectRegistersSettings GetDefault()
    {
        return new DomainObjectRegistersSettings
        {
            UseIdentitiesServices =
            {
                Value = true,
            },

            MemoryCacheDemoObjectX =
            {
                Value =
                    new MemoryCacheSettings
                    {
                        ExpirationTimeout =
                        {
                            Value = MappersCacheActualStateDtoSettings.DefaultExpirationTimeout,
                        },
                        Enabled =
                        {
                            Value = true
                        },
                        PollingInterval =
                        {
                            Value = MappersCacheActualStateDtoSettings.DefaultPollingInterval,
                        },
                        ActiveExpired =
                        {
                            Value = true
                        },
                        ExpirationMode =
                        {
                            Value = MemoryCacheSettings.ExpirationTimeoutMode.Absolute
                        },
                        FillFactor =
                        {
                            Value = 99
                        },
                        FoundBehavior =
                        {
                            Value = MemoryCacheSettings.FoundBehaviorMode.None
                        },
                        MaxItems =
                        {
                            Value = MemoryCacheMaxItems
                        },
                    }
            },
        };
    }
}