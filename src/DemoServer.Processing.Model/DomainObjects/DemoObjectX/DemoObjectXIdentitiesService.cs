﻿using System;
using System.Collections.Generic;
using System.Linq;
using ShtrihM.DemoServer.Processing.DataAccess.PostgreSql.EfModels;
using ShtrihM.DemoServer.Processing.Model.Implements;
using ShtrihM.DemoServer.Processing.Model.Interfaces;
using ShtrihM.Wattle3.DomainObjects.IdentitiesServices;

namespace ShtrihM.DemoServer.Processing.Model.DomainObjects.DemoObjectX;

public sealed class DemoObjectXIdentitiesService : BaseIdentitiesWithContextWithAlternativeKeyService<DemoObjectXAlternativeKey, long /* Group */>
{
    private readonly ICustomEntryPoint m_entryPoint;

    public DemoObjectXIdentitiesService(ICustomEntryPoint entryPoint)
        : base(
            entryPoint.Context,
            new Guid("CD1F66C3-9E12-47B4-B028-8D36BEC6D7EA"),
            initializeThreadEmergencyTimeout: entryPoint.SystemSettings.DomainObjectRegistersSettings.Value.InitializeEmergencyTimeout.Value)
    {
        m_entryPoint = entryPoint;
    }

    protected override IEnumerable<(long Identity, DemoObjectXAlternativeKey Key, long Context)> DoGetIdentitiesFromMaxToMin(long? maxIdentity)
    {
        var unitOfWork = (UnitOfWork)m_entryPoint.UnitOfWorkProvider.Instance;
        using var dbContext = unitOfWork.NewDbContext(false);

        IOrderedQueryable<Demoobjectx> query;
        if (maxIdentity.HasValue)
        {
            var currentMaxIdentity = maxIdentity.Value;
            query = dbContext
                .Demoobjectx
                .Where(a => a.Id > currentMaxIdentity)
                .OrderBy(a => a.Id);
        }
        else
        {
            query = dbContext
                .Demoobjectx
                .OrderBy(a => a.Id);
        }

        var identities =
            query.Select(
                a => new
                {
                    a.Id,
                    a.Key1,
                    a.Key2,
                    a.Group,
                });

        foreach (var identity in identities)
        {
            yield return (identity.Id, new DemoObjectXAlternativeKey(identity.Key1, identity.Key2), identity.Group);
        }
    }
}