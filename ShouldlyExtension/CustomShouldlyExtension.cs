using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Shouldly;

namespace ShouldlyExtension
{
    public static class CustomShouldlyExtension
    {
        public static void ShouldBeEquivalentTo<T>(this T firstObject, T secondObject,
            Expression<Func<T, object>>[] includeProperties, string customMessage = null)
        {
            var firstProperties = includeProperties.Any()
                ? GetProperties(firstObject, includeProperties)
                : firstObject.GetType().GetProperties().ToList();
            var secondProperties = includeProperties.Any()
                ? GetProperties(secondObject, includeProperties)
                : secondObject.GetType().GetProperties().ToList();

            if (!firstProperties.Any() || !secondProperties.Any())
                throw new ShouldAssertException(customMessage ?? "two objects does not any same properties");

            if (firstProperties.Count != secondProperties.Count)
                throw new ShouldAssertException(customMessage ?? "two objects does not any same properties");

            foreach (var firstProperty in firstProperties)
            {
                var secondProperty = secondProperties.FirstOrDefault(x => x.Name == firstProperty.Name);
                if (secondProperty == null)
                    throw new ShouldAssertException(customMessage ??
                                                    $"{secondObject.GetType().Name} does not have property " +
                                                    $"'{firstProperty.Name}'");

                var secondValue = secondProperty.GetValue(secondObject, null);
                var firstValue = firstProperty.GetValue(firstObject, null);

                if (secondValue != null && !secondValue.Equals(firstValue))
                    throw new ShouldAssertException(customMessage ??
                                                    $"{firstProperty.Name} of first object is : {firstValue} but " +
                                                    $"{secondProperty.Name} of second object is :  {secondValue}");
            }
        }

        public static void ShouldBeEquivalentTo<T>(this T obj, IEnumerable<T> objects,
            Expression<Func<T, object>>[] includeProperties, string customMessage = null) where T : class
        {
            foreach (var @object in objects)
            {
                obj.ShouldBeEquivalentTo(@object, includeProperties, customMessage);
            }
        }

        private static List<PropertyInfo> GetProperties<T>(T obj, IEnumerable<Expression<Func<T, object>>> includeProperties)
        {
            var properties = new List<PropertyInfo>();

            foreach (var includeProperty in includeProperties)
            {
                switch (includeProperty.Body)
                {
                    case UnaryExpression unaryExpression:
                    {
                        if (unaryExpression.Operand is MemberExpression memberExpression)
                        {
                            properties.Add(obj.GetType().GetProperty(memberExpression.Member.Name));
                        }

                        break;
                    }
                    case MemberExpression memberExpression:
                        properties.Add(obj.GetType().GetProperty(memberExpression.Member.Name));
                        break;
                }
            }

            return properties;
        }
    }
}