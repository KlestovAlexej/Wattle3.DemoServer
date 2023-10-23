﻿using ShtrihM.DemoServer.Processing.Model.Interfaces;
using ShtrihM.Wattle3.DomainObjects.DomainObjectDataMappers;
using ShtrihM.Wattle3.DomainObjects.DomainObjectIntergrators;
using ShtrihM.Wattle3.DomainObjects.DomainObjectsRegisters;
using ShtrihM.Wattle3.Primitives;
using ShtrihM.DemoServer.Processing.Generated.Interface;
using ShtrihM.Wattle3.DomainObjects.DomainObjectActivators;
using Unity;

namespace ShtrihM.DemoServer.Processing.Model.DomainObjects.DemoObject;

[DomainObjectIntergrator]
// ReSharper disable once UnusedType.Global
public class DomainObjectIntergratorDemoObject : BaseDomainObjectIntergrator<IUnityContainer>
{
    protected override void DoRun(IUnityContainer container)
    {
        var entryPoint = container.Resolve<ICustomEntryPoint>();
        var dataMapper =
            new DomainObjectDataMapperNoDeleteDefault
                <IMapperDemoObject, DemoObjectDtoNew, DemoObjectDtoActual, DemoObjectDtoChanged>(
                    entryPoint.Context,
                    entryPoint.SystemSettings.IdentityCachesSettings.Value.DemoObject.Value,
                    identityPrepare:
                    (mapper, identity) =>
                    {
                        var nowDayIndex = entryPoint.PartitionsDay.NowDayIndex;
                        identity = ComplexIdentity.Build(mapper.Partitions.Level, nowDayIndex, identity);

                        return identity;
                    });
        container.Resolve<DomainObjectDataMappers>().AddMapper(dataMapper);

        container.Resolve<DomainObjectRegisters>().AddRegister(
            new DomainObjectRegisterStateless(
                entryPoint.Context,
                dataMapper,
                new DomainObjectDataActivatorForActualStateDtoDefault<DemoObjectDtoActual, DomainObjectDemoObject>(
                    entryPoint),
                new DomainObjectActivatorDefault<DomainObjectDemoObject.Template, DomainObjectDemoObject>(
                    entryPoint.UnitOfWorkProvider, entryPoint.UnitOfWorkLocks.DemoObject, entryPoint),
                initializeThreadEmergencyTimeout: entryPoint.SystemSettings.DomainObjectRegistersSettings.Value
                    .InitializeEmergencyTimeout.Value));
    }
}