using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Owl.RazorTemplate
{
    public static class ReflectionHelper
    {
        private static readonly ConcurrentDictionary<string, PropertyInfo> CachedObjectProperties =
            new ConcurrentDictionary<string, PropertyInfo>();

        public static void TrySetProperty<TObject, TValue>(TObject obj, string propertyName,
            Func<TObject, TValue> valueFactory)
        {
            var cacheKey = $"{obj.GetType().FullName}{obj.GetType().BaseType?.FullName}{propertyName}";
            var property = CachedObjectProperties.GetOrAdd(cacheKey, () =>
            {
                var propertyInfo = obj.GetType().GetProperties().FirstOrDefault(x =>
                    x.Name == propertyName &&
                    x.GetSetMethod(true) != null);

                return propertyInfo == null ? null : propertyInfo;
            });

            property?.SetValue(obj, valueFactory(obj));
        }
    }
}
