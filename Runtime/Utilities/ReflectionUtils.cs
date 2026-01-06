using System;
using System.Collections.Generic;
using System.Reflection;

namespace Arunoki.Collections.Utilities
{
  public static partial class ReflectionUtils
  {
    private const BindingFlags PublicFlags = BindingFlags.Public | BindingFlags.Instance;
    private const BindingFlags ClassFlags = BindingFlags.Public | BindingFlags.NonPublic;

    public static List<Type> GetNestedTypes<T> (this Type sourceType, BindingFlags flags = ClassFlags)
      where T : class
    {
      var baseType = typeof(T);
      var nestedTypes = sourceType.GetNestedTypes (flags);
      var result = new List<Type> (nestedTypes.Length);

      for (int index = 0; index < nestedTypes.Length; index++)
      {
        var type = nestedTypes [index];

        if (!type.IsAbstract && (type == baseType || baseType.IsAssignableFrom (type)))
          result.Add (type);
      }

      return result;
    }

    public static List<T> GetAllPropertiesWithNested<T> (this object source, BindingFlags flags = PublicFlags)
      where T : class
    {
      var visited = new HashSet<T> (ReferenceEqualityComparer<T>.Instance);

      var result = new List<T> ();
      var stack = new Stack<object> ();
      stack.Push (source);

      while (stack.Count > 0)
      {
        var current = stack.Pop ();
        foreach (var obj in current.GetAllProperties<T> (flags))
        {
          if (obj == null) continue;

          if (visited.Add (obj))
          {
            result.Add (obj);
            stack.Push (obj);
          }
        }
      }

      return result;
    }

    public static List<T> GetAllProperties<T> (this object source, BindingFlags flags = PublicFlags)
      where T : class
    {
      if (source == null) throw new ArgumentNullException (nameof(source));
      var lookingType = typeof(T);
      var isType = source is Type;
      var sourceType = isType ? source as Type : source.GetType ();
      var sourceObject = isType ? null : source;

      if (isType) flags |= BindingFlags.Static; // fix issue with static singleton field

      var cachedProps = GetCachedProperties (sourceType, lookingType, flags);
      var values = new List<T> (cachedProps.Length);

      for (int i = 0; i < cachedProps.Length; i++)
      {
        var property = cachedProps [i];
        var value = (T) property.GetValue (sourceObject);
        if (value != null) values.Add (value);
      }

      return values;
    }

    #region Obsolete

    // public static List<T> GetAllPropertiesWithNested<T> (this object source, BindingFlags flags = PublicFlags)
    // {
    //   var result = new List<T> ();
    //   GetNestedTypes (source, result, flags);
    //   return result;
    // }
    //
    // private static void GetNestedTypes<T> (object source, List<T> collection, BindingFlags flags)
    // {
    //   foreach (var obj in source.GetAllProperties<T> (flags))
    //   {
    //     if (!collection.Contains (obj))
    //     {
    //       collection.Add (obj);
    //       GetNestedTypes (obj, collection, flags);
    //     }
    //   }
    // }

    //TODO: optimisation - cache PropertyInfo
    // public static List<T> GetAllProperties<T> (this object source, BindingFlags flags = PublicFlags)
    // {
    //   var lookingType = typeof(T);
    //   var isType = source is Type;
    //   var sourceType = isType ? source as Type : source.GetType ();
    //   var sourceObject = isType ? null : source;
    //   if (isType) flags |= BindingFlags.Static; // fix issue with static singleton field
    //
    //   var properties = sourceType.GetProperties (flags);
    //   var values = new List<T> (properties.Length);
    //
    //   for (int index = 0; index < properties.Length; index++)
    //   {
    //     var property = properties [index];
    //
    //     if (property.PropertyType == lookingType || lookingType.IsAssignableFrom (property.PropertyType))
    //     {
    //       var value = (T) property.GetValue (sourceObject);
    //       if (value != null) values.Add (value);
    //     }
    //   }
    //
    //   return values;
    // }

    #endregion
  }
}