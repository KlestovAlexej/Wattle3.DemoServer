﻿using System.Collections.Generic;
using System.Linq;
using ShtrihM.DemoServer.Processing.DataAccess.PostgreSql.EfModels;
using ShtrihM.DemoServer.Processing.Model.Interfaces;
using ShtrihM.Wattle3.DomainObjects.IdentitiesServices;
using ShtrihM.Wattle3.DomainObjects.Interfaces;

namespace ShtrihM.DemoServer.Processing.Model.DomainObjects.DemoObjectX;

public sealed class DemoObjectXIdentitiesService : BaseIdentitiesWithContextWithAlternativeKeyService<DomainObjectDemoObjectX.AlternativeKey, long /* Group */, DemoObjectXIdentitiesService, Demoobjectx, ProcessingDbContext>
{
    private DemoObjectXIdentitiesService(IEntryPointContext entryPointContext)
        : base(entryPointContext)
    {
    }

    public static DemoObjectXIdentitiesService? New(ICustomEntryPoint entryPoint)
    {
        var result =
            entryPoint.SystemSettings.DomainObjectRegistersSettings.Value.UseIdentitiesServices.Value
                ? new DemoObjectXIdentitiesService(entryPoint.Context)
                : null;

        return result;
    }

    protected override IEnumerable<(long Identity, DomainObjectDemoObjectX.AlternativeKey Key, long Context)> DoGetIdentities(IQueryable<Demoobjectx> entities)
    {
        var identities =
            entities.Select(
                a => new
                {
                    a.Id,
                    a.Key1,
                    a.Key2,
                    a.Group,
                });

        foreach (var identity in identities)
        {
            yield return (identity.Id, new DomainObjectDemoObjectX.AlternativeKey(identity.Key1, identity.Key2), identity.Group);
        }
    }
}
