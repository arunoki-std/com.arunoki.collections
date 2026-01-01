using System;
using System.Collections.Generic;

namespace Arunoki.Collections
{
  public class TypeSets<TElement> : BaseSet<TElement>
  {
    private readonly Dictionary<Type, Set<TElement>> setsCache = new();
    private readonly List<Set<TElement>> setsList = new();

    public TypeSets ()
    {
    }

    public TypeSets (ISetHandler<TElement> targetSetHandler) : base (targetSetHandler)
    {
    }

    protected virtual void Add (Type keyType, params TElement [] elements)
    {
      if (!setsCache.TryGetValue (keyType, out Set<TElement> set))
      {
        set = new Set<TElement> (this);
        setsList.Add (set);
        setsCache.Add (keyType, set);
      }

      foreach (var element in elements)
        set.Add (element);
    }

    protected internal virtual void Clear (Type keyType)
    {
      if (setsCache.TryGetValue (keyType, out Set<TElement> set))
      {
        set.ForEach (OnElementRemoved);
        set.Clear ();
      }
    }

    public override void RemoveWhere (Func<TElement, bool> condition)
    {
      for (int index = 0; index < setsList.Count; index++)
        setsList [index].RemoveWhere (condition);
    }

    protected internal virtual bool Remove (TElement element)
    {
      for (int index = 0; index < setsList.Count; index++)
        if (setsList [index].Remove (element))
          return true;

      return false;
    }

    public void ForEach (Predicate<TElement> condition, Action<TElement> action)
    {
      for (var i = 0; i < setsList.Count; i++)
      {
        var set = setsList [i];

        for (var index = 0; index < set.Count; index++)
          if (condition (set [index]))
            action (set [index]);
      }
    }

    public override void Cast<T> (Action<T> action)
    {
      for (var i = 0; i < setsList.Count; i++)
      {
        var set = setsList [i];

        for (var index = 0; index < set.Count; index++)
          if (set [index] is T cast)
            action (cast);
      }
    }

    public override void Cast<T> (Func<T, bool> condition, Action<T> action)
    {
      for (var i = 0; i < setsList.Count; i++)
      {
        var set = setsList [i];

        for (var index = 0; index < set.Count; index++)
          if (set [index] is T cast && condition (cast))
            action (cast);
      }
    }

    public override void ForEach (Action<TElement> action)
    {
      for (var i = 0; i < setsList.Count; i++)
      {
        var set = setsList [i];

        for (var index = 0; index < set.Count; index++)
          action (set [index]);
      }
    }

    public override void Where (Func<TElement, bool> condition, Action<TElement> action)
    {
      for (var i = 0; i < setsList.Count; i++)
      {
        var set = setsList [i];
        for (var index = 0; index < set.Count; index++)
        {
          var element = set [index];
          if (condition (element))
            action (element);
        }
      }
    }

    public Set<TElement> Get<TKeyType> ()
    {
      return setsCache [typeof(TKeyType)];
    }

    public override void Clear ()
    {
      foreach (var set in setsList)
        set.Clear ();
    }
  }
}