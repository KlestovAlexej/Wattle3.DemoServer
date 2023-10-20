﻿// <auto-generated />
using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata;
using ShtrihM.DemoServer.Processing.DataAccess.PostgreSql.EfModels;

#pragma warning disable 219, 612, 618
#nullable disable

namespace ShtrihM.DemoServer.Processing.DataAccess.PostgreSql.EfModelsOptimized
{
    internal partial class PdDemoObjectEntityType
    {
        public static RuntimeEntityType Create(RuntimeModel model, RuntimeEntityType baseEntityType = null)
        {
            var runtimeEntityType = model.AddEntityType(
                "ShtrihM.DemoServer.Processing.DataAccess.PostgreSql.EfModels.PdDemoObject",
                typeof(PdDemoObject),
                baseEntityType);

            var id = runtimeEntityType.AddProperty(
                "Id",
                typeof(long),
                propertyInfo: typeof(PdDemoObject).GetProperty("Id", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(PdDemoObject).GetField("<Id>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                afterSaveBehavior: PropertySaveBehavior.Throw);
            id.AddAnnotation("Relational:ColumnName", "id");

            var createdate = runtimeEntityType.AddProperty(
                "Createdate",
                typeof(DateTime),
                propertyInfo: typeof(PdDemoObject).GetProperty("Createdate", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(PdDemoObject).GetField("<Createdate>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
            createdate.AddAnnotation("Relational:ColumnName", "createdate");
            createdate.AddAnnotation("Relational:ColumnType", "timestamp without time zone");

            var enabled = runtimeEntityType.AddProperty(
                "Enabled",
                typeof(bool),
                propertyInfo: typeof(PdDemoObject).GetProperty("Enabled", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(PdDemoObject).GetField("<Enabled>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
            enabled.AddAnnotation("Relational:ColumnName", "enabled");

            var modificationdate = runtimeEntityType.AddProperty(
                "Modificationdate",
                typeof(DateTime),
                propertyInfo: typeof(PdDemoObject).GetProperty("Modificationdate", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(PdDemoObject).GetField("<Modificationdate>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
            modificationdate.AddAnnotation("Relational:ColumnName", "modificationdate");
            modificationdate.AddAnnotation("Relational:ColumnType", "timestamp without time zone");

            var name = runtimeEntityType.AddProperty(
                "Name",
                typeof(string),
                propertyInfo: typeof(PdDemoObject).GetProperty("Name", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(PdDemoObject).GetField("<Name>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                maxLength: 1024);
            name.AddAnnotation("Relational:ColumnName", "name");

            var revision = runtimeEntityType.AddProperty(
                "Revision",
                typeof(long),
                propertyInfo: typeof(PdDemoObject).GetProperty("Revision", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(PdDemoObject).GetField("<Revision>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
            revision.AddAnnotation("Relational:ColumnName", "revision");

            var key = runtimeEntityType.AddKey(
                new[] { id });
            runtimeEntityType.SetPrimaryKey(key);

            return runtimeEntityType;
        }

        public static void CreateAnnotations(RuntimeEntityType runtimeEntityType)
        {
            runtimeEntityType.AddAnnotation("Relational:FunctionName", null);
            runtimeEntityType.AddAnnotation("Relational:Schema", null);
            runtimeEntityType.AddAnnotation("Relational:SqlQuery", null);
            runtimeEntityType.AddAnnotation("Relational:TableName", "demoobject");
            runtimeEntityType.AddAnnotation("Relational:ViewName", null);
            runtimeEntityType.AddAnnotation("Relational:ViewSchema", null);

            Customize(runtimeEntityType);
        }

        static partial void Customize(RuntimeEntityType runtimeEntityType);
    }
}