﻿using System;

namespace ShtrihM.DemoServer.Processing.DataAccess.PostgreSql.EfModels;

public class Systemsetting
{
    public Guid Id { get; set; }

    public string Value { get; set; }

    public string Name { get; set; }
}