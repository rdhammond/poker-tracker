using PokerTracker.DAL.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PokerTracker.DAL.Extensions
{
    public static class TypeToSql
    {
        private static T GetCustomAttribute<T>(this Type t)
            where T : Attribute
        {
            return (T)t.GetCustomAttributes(typeof(T), true).FirstOrDefault();
        }

        public static string GetTableName(this Type t)
        {
            var attr = t.GetCustomAttribute<TableNameAttribute>();

            if (attr == null)
                throw new ArgumentException("Class must be decorated with TableNameAttribute.");

            return attr.Name;
        }

        public static string GetIdField(this Type t)
        {
            foreach (var p in GetProperties(t))
            {
                if (p.GetCustomAttribute<IdFieldAttribute>() != null)
                    return p.Name;
            }

            throw new ArgumentException("One public property must be decorated with IdFieldAttribute.");
        }

        private static IEnumerable<PropertyInfo> GetProperties(this Type t)
        {
            return
                from p in t.GetProperties()
                where p.GetMethod != null
                select p;
        }

        public static IEnumerable<string> GetPublicPropertyNames(this Type t)
        {
            return from p in GetProperties(t) select p.Name;
        }
    }
}
