﻿using Microsoft.Extensions.Logging;
using ShtrihM.DemoServer.Processing.Api.Common;
using ShtrihM.DemoServer.Processing.Common;
using ShtrihM.DemoServer.Processing.Model.Interfaces;
using ShtrihM.Wattle3.DomainObjects.DomainObjects;
using ShtrihM.Wattle3.DomainObjects.Interfaces;
using ShtrihM.Wattle3.Mappers.Primitives.MutableFields;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using ShtrihM.DemoServer.Processing.Generated.Interface;
using ShtrihM.Wattle3.DomainObjects;
using ShtrihM.Wattle3.DomainObjects.DomainObjectActivators;
using ShtrihM.DemoServer.Processing.Model.DomainObjects.Common;
using ShtrihM.Wattle3.DomainObjects.UnitOfWorkLocks;
using ShtrihM.Wattle3.Utils;
using ShtrihM.Wattle3.Common.DomainObjects;

namespace ShtrihM.DemoServer.Processing.Model.DomainObjects.DemoObjectX;

[DomainObjectDataMapper]
// ReSharper disable once ClassNeverInstantiated.Global
public sealed class DomainObjectDemoObjectX : BaseDomainObjectMutableWithUpdateLock<DomainObjectDemoObjectX>, IDomainObjectDemoObjectX,
    IDomainObjectActivatorPostCreate
{
    #region AlternativeKey - альтернативный ключ объекта DemoObjectX

    /// <summary>
    /// Альтернативный ключ объекта DemoObjectX по : <see cref="Key1"/>, <see cref="Key2"/>.
    /// </summary>
    /// <param name="Key1">Альтернативный ключ - часть №1.</param>
    /// <param name="Key2">Альтернативный ключ - часть №2/</param>
    public readonly record struct AlternativeKey(
        // ReSharper disable once NotAccessedPositionalProperty.Global
        [property: AlternativeKeyIndex(WellknownDomainObjectFields.DemoObjectX.IndexAlternateKeyValue1)] Guid Key1,
        // ReSharper disable once NotAccessedPositionalProperty.Global
        [property: AlternativeKeyIndex(WellknownDomainObjectFields.DemoObjectX.IndexAlternateKeyValue2)] string Key2);

    #endregion

    #region Template - шаблон создания объекта DemoObjectX

    /// <summary>
    /// Шаблон создания объекта <see cref="DomainObjectDemoObjectX"/>.
    /// </summary>
    public class Template : IDomainObjectTemplate
    {
        public Template(
            string name,
            bool enabled,
            Guid key1,
            string key2,
            long group)
        {
            Name = name;
            Enabled = enabled;
            Key1 = key1;
            Key2 = key2;
            Group = group;
            Key2 = key2;
        }

        public readonly string Name;
        public readonly bool Enabled;
        public readonly Guid Key1;
        public readonly string Key2;
        public readonly long Group;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AlternativeKey GetKey()
        {
            var result = new AlternativeKey(Key1, Key2);

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IDomainObjectDemoObjectX New(ICustomEntryPoint entryPoint)
        {
            return entryPoint.CurrentUnitOfWork.New<IDomainObjectDemoObjectX>(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ValueTask<IDomainObjectDemoObjectX> NewAsync(ICustomEntryPoint entryPoint, CancellationToken cancellationToken = default)
        {
            return entryPoint.CurrentUnitOfWork.NewAsync<IDomainObjectDemoObjectX>(this, cancellationToken);
        }
    }

    #endregion

    #region Изменяемы поля

    [DomainObjectFieldValue]
    private MutableField<bool> m_enabled;

    [DomainObjectFieldValue]
    private MutableFieldStringLimitedEx m_name;

    #endregion

    #region Конструкторы

    // ReSharper disable once UnusedMember.Global
    public DomainObjectDemoObjectX(
        DemoObjectXDtoActual data,
        ICustomEntryPoint entryPoint,
        IDomainObjectUnitOfWorkLockService lockUpdate)
        : base(entryPoint, data, lockUpdate)
    {
        CreateDate = data.CreateDate;
        ModificationDate = data.ModificationDate;
        m_name = new MutableFieldStringLimitedEx(FieldsConstants.DemoObjectXNameMaxLength, data.Name);
        m_enabled = new MutableField<bool>(data.Enabled);
        Key1 = data.Key1;
        Key2 = data.Key2;
        Group = data.Group;
    }

    // ReSharper disable once UnusedMember.Global
    public DomainObjectDemoObjectX(
        long identity,
        Template template,
        ICustomEntryPoint entryPoint,
        IDomainObjectUnitOfWorkLockService lockUpdate)
        : base(entryPoint, identity, lockUpdate)
    {
        CreateDate = m_entryPoint.TimeService.Now;
        ModificationDate = CreateDate;
        m_name = new MutableFieldStringLimitedEx(FieldsConstants.DemoObjectXNameMaxLength, template.Name);
        m_enabled = new MutableField<bool>(template.Enabled);
        Key1 = template.Key1;
        Key2 = template.Key2;
        Group = template.Group;
    }

    #endregion

    public override Guid TypeId => WellknownDomainObjects.DemoObjectX;

    public bool Enabled
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => m_enabled.Value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            m_lockUpdate.Has(Identity);

            m_enabled.SetValue(value);
            if (m_enabled.Changed)
            {
                DoUpdateAsync().SafeGetResult();
            }
        }
    }

    public string Name
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => m_name.Value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            m_lockUpdate.Has(Identity);

            m_name.SetValue(value);
            if (m_name.Changed)
            {
                DoUpdateAsync().SafeGetResult();
            }
        }
    }

    [DomainObjectFieldValue]
    public DateTimeOffset CreateDate
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get;
    }

    [DomainObjectFieldValue]
    public Guid Key1
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get;
    }

    [DomainObjectFieldValue]
    public string Key2
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get;
    }

    [DomainObjectFieldValue]
    public long Group
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get;
    }

    [DomainObjectFieldValue]
    public DateTimeOffset ModificationDate
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private set;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AlternativeKey GetKey()
    {
        var result = new AlternativeKey(Key1, Key2);

        return result;
    }

    public void Delete()
    {
        m_entryPoint.CurrentUnitOfWork.AddDelete(this);
    }

    protected override ValueTask DoUpdateAsync(CancellationToken cancellationToken = default)
    {
        ModificationDate = m_entryPoint.TimeService.Now;

        return base.DoUpdateAsync(cancellationToken);
    }

    #region Аспект создания

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void PostCreate()
    {
        var unitOfWork = m_entryPoint.CurrentUnitOfWork;
        var domainBehaviour = unitOfWork.CreateDomainBehaviourWithСonfirmation();
        unitOfWork.AddBehaviour(domainBehaviour);

        var identity = Identity;

        domainBehaviour.SetFailAll(
            () =>
            {
                var logger = m_entryPoint.LoggerFactory.CreateLogger<DomainObjectDemoObjectX>();
                var messgae = $"Не удалось создать объект X с идентификатором '{identity}'.";
                logger.LogError(messgae);
                Console.WriteLine(messgae);
            });

        domainBehaviour.SetSuccessfulAll(
            () =>
            {
                var logger = m_entryPoint.LoggerFactory.CreateLogger<DomainObjectDemoObjectX>();
                var messgae = $"Создан объект X с идентификатором '{identity}'.";
                logger.LogError(messgae);
                Console.WriteLine(messgae);
            });
    }

    public ValueTask PostCreateAsync(CancellationToken cancellationToken)
    {
        PostCreate();

        return ValueTask.CompletedTask;
    }

    #endregion
}