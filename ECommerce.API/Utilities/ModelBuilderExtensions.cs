using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Entities;
using Pluralize.NET.Core;

namespace API.Utilities
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Singularizin table name like Posts to Post or People to Person
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void AddSingularizingTableNameConvention(this ModelBuilder modelBuilder)
        {
            Pluralizer pluralizer = new Pluralizer();
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                string tableName = entityType.GetTableName();
                entityType.SetTableName(pluralizer.Singularize(tableName));
            }
        }

        /// <summary>
        /// Pluralizing table name like Post to Posts or Person to People
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="exceptedClassesFromPlurize"></param>
        public static void AddPluralizingTableNameConvention(this ModelBuilder modelBuilder, List<string> exceptedClassesFromPlurize)
        {
            Pluralizer pluralizer = new Pluralizer();
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                string tableName = entityType.GetTableName();
                if (exceptedClassesFromPlurize.Contains(tableName))
                    entityType.SetTableName(tableName);
                else
                    entityType.SetTableName(pluralizer.Pluralize(tableName));
            }
        }

        /// <summary>
        /// This method will change identity table names like AspNetUserClaim to UserClaim
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void ConfigureIdentityTableName(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users", "Security");
            modelBuilder.Entity<UserRole>().ToTable("UserRoles", "Security");
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogin", "Security");
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaim", "Security");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UserRole", "Security");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UserToken", "Security");
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaim", "Security");
        }

        /// <summary>
        /// Set NEWSEQUENTIALID() sql function for all columns named "Id"
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="mustBeIdentity">Set to true if you want only "Identity" guid fields that named "Id"</param>
        public static void AddSequentialGuidForIdConvention(this ModelBuilder modelBuilder)
        {
            //modelBuilder.AddDefaultValueSqlConvention("Id", typeof(int), "NEWSEQUENTIALID()");
        }

        /// <summary>
        /// Set DefaultValueSql for sepecific property name and type
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="propertyName">Name of property wants to set DefaultValueSql for</param>
        /// <param name="propertyType">Type of property wants to set DefaultValueSql for </param>
        /// <param name="defaultValueSql">DefaultValueSql like "NEWSEQUENTIALID()"</param>
        public static void AddDefaultValueSqlConvention(this ModelBuilder modelBuilder, string propertyName, Type propertyType, string defaultValueSql)
        {
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                IMutableProperty property = entityType.GetProperties().SingleOrDefault(p => p.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));
                if (property != null && property.ClrType == propertyType)
                {
                    property.SetDefaultValueSql(defaultValueSql);
                }
            }
        }

        /// <summary>
        /// Set DeleteBehavior.Restrict by default for relations
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void AddRestrictDeleteBehaviorConvention(this ModelBuilder modelBuilder)
        {
            IEnumerable<IMutableForeignKey> cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (IMutableForeignKey fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        /// <summary>
        /// Dynamicaly load all IEntityTypeConfiguration with Reflection
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="assemblies">Assemblies contains Entities</param>
        public static void RegisterEntityTypeConfiguration(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            MethodInfo applyGenericMethod = typeof(ModelBuilder).GetMethods().First(m => m.Name == nameof(ModelBuilder.ApplyConfiguration));

            IEnumerable<Type> types = assemblies.SelectMany(a => a.GetExportedTypes())
                .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic);

            foreach (Type type in types)
            {
                foreach (Type iface in type.GetInterfaces())
                {
                    if (iface.IsConstructedGenericType && iface.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                    {
                        MethodInfo applyConcreteMethod = applyGenericMethod.MakeGenericMethod(iface.GenericTypeArguments[0]);
                        applyConcreteMethod.Invoke(modelBuilder, new object[] { Activator.CreateInstance(type) });
                    }
                }
            }
        }

        /// <summary>
        /// Dynamicaly register all Entities that inherit from specific BaseType
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="baseType">Base type that Entities inherit from this</param>
        /// <param name="assemblies">Assemblies contains Entities</param>
        public static void RegisterAllEntities<BaseType>(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            IEnumerable<Type> types = assemblies.SelectMany(a => a.GetExportedTypes())
                .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic && typeof(BaseType).IsAssignableFrom(c));

            foreach (Type type in types)
            {
                modelBuilder.Entity(type);
            }
        }


        /// <summary>
        /// Protect SQL Data Column If Property Have ProtectedAttribute
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="dataProtectionProvider"></param>
        /// <param name="configRoot"></param>
        public static void AddColumnProtector(this ModelBuilder modelBuilder, IDataProtectionProvider dataProtectionProvider, IConfiguration configRoot)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    var attributes = property.PropertyInfo.GetCustomAttributes(typeof(ProtectedAttribute), false);
                    if (attributes.Any())
                    {

                        property.SetValueConverter(new ProtectedConverter(dataProtectionProvider, configRoot));
                    }
                }
            }
        }
    }
}
