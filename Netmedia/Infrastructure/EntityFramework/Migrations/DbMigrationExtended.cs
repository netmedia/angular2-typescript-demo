using System;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Data.Entity.Migrations;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Netmedia.Common.Extensions;

namespace Netmedia.Infrastructure.EntityFramework.Migrations
{
    public abstract class DbMigrationExtended : DbMigration
    {
        private StringBuilder _commandBuilder;

        protected void Line(string sqlLine)
        {
            if (_commandBuilder == null) _commandBuilder = new StringBuilder();

            _commandBuilder.AppendFormat("{0}{1}", sqlLine, Environment.NewLine);
        }

        protected void CreateStoredProcedure(string procedure, params string[] bodyLines)
        {
            foreach (var line in bodyLines)
            {
                Line(line);
            }

            CreateProcedureFromAssembledSql(procedure);
        }
        protected void CreateProcedureFromAssembledSql(string procedureName)
        {
            base.CreateStoredProcedure(procedureName, _commandBuilder.ToString());

            _commandBuilder = null;
        }
        protected void ExecuteCommand(params string[] bodyLines)
        {
            foreach (var line in bodyLines)
            {
                Line(line);
            }

            ExecuteAssembledSql();
        }
        protected void ExecuteAssembledSql()
        {
            Sql(_commandBuilder.ToString());

            _commandBuilder = null;
        }

        protected void UpdateEnumMembers<EntityPropertyType>()
        {
            var plural = new EnglishPluralizationService();
            var enumTableName = plural.Pluralize(typeof(EntityPropertyType).Name);
            var commandBuilder = new StringBuilder();

            commandBuilder.AppendFormat("set identity_insert dbo.{0} on{1}", enumTableName, Environment.NewLine);
            foreach (string value in Enum.GetNames(typeof(EntityPropertyType)))
            {
                var id = (int) Enum.Parse(typeof (EntityPropertyType), value);

                commandBuilder.AppendFormat("if not exists (select * from dbo.{0} where Id = {1}){2}", enumTableName, id, Environment.NewLine);
                commandBuilder.AppendFormat("begin{0}", Environment.NewLine);
                commandBuilder.AppendFormat("insert into dbo.{0} (Id, Code, Name, CreatedOnDate, ModifiedOnDate) values ({1}, '{2}', '{3}', getdate(), getdate()){4}", enumTableName, id, value, _GetPhraseFromString(value), Environment.NewLine);
                commandBuilder.AppendFormat("end{0}", Environment.NewLine);
            }
            commandBuilder.AppendFormat("set identity_insert dbo.{0} off{1}", enumTableName, Environment.NewLine);

            Sql(commandBuilder.ToString());
        }

        // Todo: we are missing an update mechanism
        // and a way to not create a table if it already exists
        // and some way to move this out to make reusable
        protected void AddEnumAndForeignKey<EntityType, EntityPropertyType>(Expression<Func<EntityType, EntityPropertyType>> selector, bool skipCreatingTable = false, string overridenFkTableName = "")
        {
            var type = typeof(EntityPropertyType);
            try
            {
                type = (type.GetGenericTypeDefinition() == typeof(Nullable<>)) ? Nullable.GetUnderlyingType(type) : type; 
            }
            catch (Exception ex)
            {
                //
            }

            var plural = new EnglishPluralizationService();
            var enumTableName = plural.Pluralize(type.Name);
            var foreignKeyTableName = overridenFkTableName.IsNullOrEmpty() ? plural.Pluralize(typeof(EntityType).Name) : overridenFkTableName;
            var propertyName = _GetPropertyName(selector) + "Id";

            AddEnumAndForeignKey<EntityPropertyType>(enumTableName, foreignKeyTableName, propertyName, skipCreatingTable);
        }
        protected void AddEnumAndForeignKey<EntityPropertyType>(string enumTableName, string foreignKeyTableName, string propertyName, bool skipCreatingTable = false)
        {
            var foreignKeyName = "FK_" + foreignKeyTableName + "_" + enumTableName;

            if (skipCreatingTable == false)
            {
                CreateTable("dbo." + enumTableName,
                    c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        CreatedOnDate = c.DateTime(nullable: false),
                        ModifiedOnDate = c.DateTime(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        ModifiedById = c.String(maxLength: 128),
                    })
                    .PrimaryKey(t => t.Id)
                    .ForeignKey("dbo.Users", t => t.CreatedById)
                    .ForeignKey("dbo.Users", t => t.ModifiedById)
                    .Index(t => t.CreatedById)
                    .Index(t => t.ModifiedById);

                var _type = typeof(EntityPropertyType);
                try
                {
                    _type = typeof(EntityPropertyType).GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(typeof(EntityPropertyType)) : _type;
                }
                catch (Exception ex)
                {
                    //
                }
                
                foreach (string value in Enum.GetNames(_type))
                {
                    Sql("insert into dbo." + enumTableName + " (Code, Name, CreatedOnDate, ModifiedOnDate) values ('" + value + "', '" + _GetPhraseFromString(value) + "', getdate(), getdate())");
                }
            }
            AddForeignKey(foreignKeyTableName, propertyName, enumTableName, "Id", name: foreignKeyName);
        }

        protected void DropEnumAndForeignKey(string enumTableName, string foreignKeyTableName, string propertyName, bool skipDroppingTable = false)
        {
            var foreignKeyName = "FK_" + foreignKeyTableName + "_" + enumTableName;

            DropForeignKey(foreignKeyTableName, foreignKeyName);

            if (skipDroppingTable == false)
                DropTable(enumTableName);
        }
        protected void DropEnumAndForeignKey<EntityType, EntityPropertyType>(Expression<Func<EntityType, EntityPropertyType>> selector, bool skipDroppingTable = false, string overridenFkTableName = "")
        {
            var type = typeof(EntityPropertyType);
            type = (type.GetGenericTypeDefinition() == typeof(Nullable<>)) ? Nullable.GetUnderlyingType(type) : type; 

            var plural = new EnglishPluralizationService();
            var enumTableName = plural.Pluralize(type.Name);
            var foreignKeyTableName = overridenFkTableName.IsNullOrEmpty() ? plural.Pluralize(typeof(EntityType).Name) : overridenFkTableName;
            var propertyName = _GetPropertyName(selector) + "Id";

            DropEnumAndForeignKey(enumTableName, foreignKeyTableName, propertyName, skipDroppingTable);
        }

        private string _GetPhraseFromString(string words)
        {
            return string.Join(" ", Regex.Split(words, @"([A-Z][a-z]*)"));
        }
        private string _GetPropertyName<EntityType, EntityPropertyType>(Expression<Func<EntityType, EntityPropertyType>> selector)
        {
            var propertyName = "NoPropertyFound"; //(string)null;

            if (selector.Body.NodeType == ExpressionType.MemberAccess)
            {
                var memberExpression = (MemberExpression)selector.Body;
                //propertyName = memberExpression.Member is PropertyInfo ? memberExpression.Member.Name : (string)null;
                propertyName = memberExpression.ToString();
                propertyName = propertyName.Substring(propertyName.IndexOf(".") + 1);
            }

            return propertyName.IsNotNullOrEmpty() ? propertyName.Replace(".Value", "") : "";
        }

    }
}
