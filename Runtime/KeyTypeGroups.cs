using System;
using System.Collections.Generic;

namespace Arunoki.Collections
{
  public class KeyTypeGroups<TElement> : BaseSet<TElement>
  {
    protected internal Dictionary<Type, Set<TElement>> TypeSetts = new();
    private readonly List<Set<TElement>> cache = new();

    public KeyTypeGroups ()
    {
    }

    public KeyTypeGroups (ISetHandler<TElement> targetSetHandler) : base (targetSetHandler)
    {
    }

    protected virtual void Add (Type keyType, params TElement [] elements)
    {
      if (!TypeSetts.TryGetValue (keyType, out Set<TElement> set))
      {
        set = new Set<TElement> (this);
        cache.Add (set);
        TypeSetts.Add (keyType, set);
      }

      foreach (var element in elements)
        set.Add (element);
    }

    protected internal virtual void Clear (Type keyType)
    {
      if (TypeSetts.TryGetValue (keyType, out Set<TElement> set))
      {
        set.ForEach (OnElementRemoved);
        set.Clear ();
      }
    }

    protected internal virtual bool Remove (TElement element)
    {
      for (int index = 0; index < cache.Count; index++)
        if (cache [index].Remove (element))
          return true;

      return false;
    }

    public void ForEach (Predicate<TElement> condition, Action<TElement> action)
    {
      foreach (var set in cache)
        for (var i = set.Count - 1; i >= 0; i--)
          if (condition (set [i]))
            action (set [i]);
    }

    public override void Cast<T> (Action<T> action)
    {
      for (var i = 0; i < cache.Count; i++)
      {
        var set = cache [i];
        for (var index = set.Count - 1; index >= 0; index--)
          if (set [index] is T cast)
            action (cast);
      }
    }

    public override void Cast<T> (Func<T, bool> condition, Action<T> action)
    {
      for (var i = 0; i < cache.Count; i++)
      {
        var set = cache [i];
        for (var index = set.Count - 1; index >= 0; index--)
          if (set [index] is T cast && condition (cast))
            action (cast);
      }
    }

    public override void ForEach (Action<TElement> action)
    {
      for (var i = 0; i < cache.Count; i++)
      {
        var set = cache [i];
        for (var index = set.Count - 1; index >= 0; index--)
          action (set [index]);
      }
    }

    public override void Where (Func<TElement, bool> condition, Action<TElement> action)
    {
      for (var i = 0; i < cache.Count; i++)
      {
        var set = cache [i];
        for (var index = set.Count - 1; index >= 0; index--)
        {
          var element = set [index];
          if (condition (element)) action (element);
        }
      }
    }

    public Set<TElement> Get<TKeyType> ()
    {
      return TypeSetts [typeof(TKeyType)];
    }

    public override void Clear ()
    {
      foreach (var set in cache)
        set.Clear ();
    }
  }
}