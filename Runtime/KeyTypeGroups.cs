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

    public override IEnumerable<T> GetAll<T> ()
    {
      foreach (var set in cache)
        for (var i = set.Count - 1; i >= 0; i--)
          if (set [i] is T match)
            yield return match;
    }

    public override IEnumerable<TElement> GetAll ()
    {
      foreach (var set in cache)
        for (var i = set.Count - 1; i >= 0; i--)
          yield return set [i];
    }

    public override IEnumerable<T> Where<T> (Predicate<T> condition)
    {
      foreach (var set in cache)
        for (var i = set.Count - 1; i >= 0; i--)
          if (set [i] is T match && condition (match))
            yield return match;
    }

    public override IEnumerable<TElement> Where (Predicate<TElement> condition)
    {
      foreach (var set in cache)
        for (var i = set.Count - 1; i >= 0; i--)
          if (condition (set [i]))
            yield return set [i];
    }

    public override void ForEach (Predicate<TElement> condition, Action<TElement> action)
    {
      foreach (var set in cache)
        for (var i = set.Count - 1; i >= 0; i--)
          if (condition (set [i]))
            action (set [i]);
    }

    public override void ForEachOf<T> (Predicate<T> condition, Action<T> action)
    {
      foreach (var set in cache)
        for (var i = set.Count - 1; i >= 0; i--)
          if (set [i] is T match && condition (match))
            action (match);
    }

    public override void ForEach (Action<TElement> action)
    {
      foreach (var set in cache)
        for (var i = set.Count - 1; i >= 0; i--)
          action (set [i]);
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