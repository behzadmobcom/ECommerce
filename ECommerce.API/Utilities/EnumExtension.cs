using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace API.Utilities
{
    public static class EnumExtensions
    {
        public static IEnumerable<T> GetEnumValues<T>(this T input) where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new NotSupportedException();
            }

            return Enum.GetValues(input.GetType()).Cast<T>();
        }

        public static IEnumerable<T> GetEnumFlags<T>(this T input) where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new NotSupportedException();
            }

            foreach (var value in Enum.GetValues(input.GetType()))
            {
                if ((input as Enum).HasFlag(value as Enum))
                {
                    yield return (T)value;
                }
            }
        }

        public static string ToDisplay(this Enum value, DisplayProperty property = DisplayProperty.Name)
        {
            Assert.NotNull(value, nameof(value));

            var attribute = value.GetType().GetField(value.ToString()).GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();
            if (attribute == null)
            {
                return value.ToString();
            }

            return attribute.GetType().GetProperty(property.ToString()).GetValue(attribute, null).ToString();
        }

        public static Dictionary<int, string> ToDictionary(this Enum value)
        {
            return Enum.GetValues(value.GetType()).Cast<Enum>().ToDictionary(p => Convert.ToInt32(p), q => ToDisplay(q));
        }

        public static IEnumerable<T> EnumGetOrderedValues<T>(this Type enumType)
        {
            var fields = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);
            var orderedValues = new List<Tuple<int, T>>();
            foreach (var field in fields)
            {
                var orderAtt = field.GetCustomAttributes(typeof(OrderAttribute), false).SingleOrDefault() as OrderAttribute;
                if (orderAtt != null)
                {
                    orderedValues.Add(new Tuple<int, T>(orderAtt.Order, (T)field.GetValue(null)));
                }
            }

            return orderedValues.OrderBy(x => x.Item1).Select(x => x.Item2).ToList();
        }

        public static List<EnumField> GetEnumFields(this Type enumType)
        {
            //var fields = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);

            var items = new List<EnumField>();

            foreach (var name in enumType.GetEnumNames())
            {
                var orderAtt = enumType.GetField(name).GetCustomAttributes(typeof(OrderAttribute), false).SingleOrDefault() as OrderAttribute;

                var displayNameAtt = enumType.GetField(name).GetCustomAttributes(typeof(NameAttribute), false).SingleOrDefault() as NameAttribute;

                var valueAtt = enumType.GetField(name).GetCustomAttributes(typeof(ValueAttribute), false).SingleOrDefault() as ValueAttribute;

                if (orderAtt != null && displayNameAtt != null && valueAtt != null)
                {
                    EnumField fieldinfo = new EnumField { Order = (int)orderAtt.Order, Name = displayNameAtt.Name, Value = valueAtt.Value };

                    items.Add(fieldinfo);
                }
            }


            //foreach (var field in fields)
            //{
            //    var orderAtt = field.GetCustomAttributes(typeof(OrderAttribute), false).SingleOrDefault() as OrderAttribute;
            //    if (orderAtt != null)
            //    {
            //        EnumField fieldinfo = new EnumField { Order = (int)orderAtt.Order, Name = field.Name, Value = (int)field.GetValue(null) };

            //        items.Add(fieldinfo);
            //    }
            //}

            return items;
        }
    }

    public enum DisplayProperty
    {
        Description,
        GroupName,
        Name,
        Prompt,
        ShortName,
        Order
    }

    public class OrderAttribute : Attribute
    {
        public readonly int Order;

        public OrderAttribute(int order)
        {
            Order = order;
        }
    }

    public class NameAttribute : Attribute
    {
        public readonly string Name;

        public NameAttribute(string name)
        {
            Name = name;
        }
    }

    public class ValueAttribute : Attribute
    {
        public readonly int Value;

        public ValueAttribute(int value)
        {
            Value = value;
        }
    }

    public class EnumField
    {
        public int Order { get; set; }
        public int Value { get; set; }
        public string Name { get; set; }
    }
}