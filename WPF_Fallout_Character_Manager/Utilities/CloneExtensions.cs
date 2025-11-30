using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WPF_Fallout_Character_Manager.Models.External.Inventory;

namespace WPF_Fallout_Character_Manager.Utilities
{
    //public static class DeepCloner
    //{
    //    private static readonly Dictionary<Type, FieldInfo[]> CachedFields = new();

    //    public static T Clone<T>(this T source)
    //    {
    //        var visited = new Dictionary<object, object>(ReferenceEqualityComparer.Instance);
    //        return (T)CloneObject(source!, visited)!;
    //    }

    //    private static object? CloneObject(object? original, Dictionary<object, object> visited)
    //    {
    //        if (original is null)
    //            return null;

    //        var type = original.GetType();

    //        // Primitive + immutable types can be returned directly
    //        if (type.IsPrimitive || type.IsEnum || type == typeof(string) || type == typeof(decimal))
    //            return original;

    //        // Already cloned → return cached
    //        if (visited.TryGetValue(original, out var existing))
    //            return existing;

    //        // Arrays
    //        if (type.IsArray)
    //        {
    //            var array = (Array)original;
    //            var clone = Array.CreateInstance(type.GetElementType()!, array.Length);
    //            visited[original] = clone;

    //            for (int i = 0; i < array.Length; i++)
    //                clone.SetValue(CloneObject(array.GetValue(i), visited), i);

    //            return clone;
    //        }

    //        // Observable Collection
    //        if(IsObservableCollection(type))
    //        {
    //            return CloneObservableCollection(original, type, visited);
    //        }

    //        // Create instance
    //        var cloneObj = Activator.CreateInstance(type)!;
    //        visited[original] = cloneObj;


    //        foreach (var field in GetAllFields(type))
    //        {
    //            // Skip event handlers
    //            if (typeof(Delegate).IsAssignableFrom(field.FieldType))
    //                continue;

    //            var value = field.GetValue(original);
    //            var clonedValue = CloneObject(value, visited);
    //            field.SetValue(cloneObj, clonedValue);
    //        }

    //        return cloneObj;
    //    }

    //    private static bool IsObservableCollection(Type type)
    //    {
    //        return type.IsGenericType && type.GetGenericTypeDefinition().Name.StartsWith("ObservableCollection");
    //    }

    //    private static object CloneObservableCollection(object original, Type type, Dictionary<object, object> visited)
    //    {
    //        var elementType = type.GetGenericArguments()[0];
    //        var clone = (IList)Activator.CreateInstance(type)!;

    //        visited[original] = clone;

    //        foreach (var item in (IEnumerable)original)
    //            clone.Add(CloneObject(item, visited));

    //        return clone;
    //    }

    //    private static FieldInfo[] GetAllFields(Type type)
    //    {
    //        if (CachedFields.TryGetValue(type, out var fields))
    //            return fields;

    //        List<FieldInfo> result = new();

    //        while (type != null && type != typeof(object))
    //        {
    //            result.AddRange(
    //                type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly)
    //            );

    //            type = type.BaseType!;
    //        }

    //        CachedFields[type] = result.ToArray();
    //        return result.ToArray();
    //    }
    //}

    //public static class CloneExtensions
    //{
    //    public static T DeepClone<T>(this T source)
    //    {
    //        var clone = DeepCloner.Clone(source);

    //        if (clone is IRebindEvents rebinder)
    //            rebinder.RebindEvents();

    //        return clone;
    //    }
    //}

    //public static class CloneExtensions
    //{
    //    private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
    //    {
    //        IncludeFields = true,
    //        WriteIndented = false
    //    };

    //    public static T DeepClone<T>(this T source)
    //    {
    //        if (source == null)
    //            return default!;

    //        var json = JsonSerializer.Serialize(source, Options);
    //        var clone = JsonSerializer.Deserialize<T>(json, Options)!;

    //        if(clone is IRebindEvents rebinder)
    //        {
    //            rebinder.RebindEvents();
    //        }

    //        return clone;
    //    }
    //}
}
