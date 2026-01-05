using System;
using System.Collections.Generic;

namespace Arunoki.Collections
{
  public class SetsTypeCollection<TElement> : Container<TElement>, ISet<TElement>
  {
    private readonly Dictionary<Type, Set<TElement>> setsCache = new();
    private readonly List<Set<TElement>> setsList = new();

    public SetsTypeCollection () : this (null) { }
    public SetsTypeCollection (IContainer<TElement> container) : base (container) { }

    public int Count
    {
      get
      {
        var count = 0;
        for (var i = 0; i < setsList.Count; i++)
          count += setsList [i].Count;
        return count;
      }
    }

    public void Add (Type keyType, params TElement [] elements)
    {
      var set = GetOrCreate (keyType);

      for (var i = 0; i < elements.Length; i++)
        set.Add (elements [i]);
    }

    public void Add (Type keyType, TElement element)
    {
      GetOrCreate (keyType).Add (element);
    }

    public void RemoveWhere (Func<TElement, bool> condition)
    {
      for (int index = 0; index < setsList.Count; index++)
        setsList [index].RemoveWhere (condition);
    }

    public bool Remove (TElement element)
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

    public void Cast<T> (Action<T> action)
    {
      for (var i = 0; i < setsList.Count; i++)
      {
        var set = setsList [i];

        for (var index = 0; index < set.Count; index++)
          if (set [index] is T cast)
            action (cast);
      }
    }

    public void Cast<T> (Func<T, bool> condition, Action<T> action)
    {
      for (var i = 0; i < setsList.Count; i++)
      {
        var set = setsList [i];

        for (var index = 0; index < set.Count; index++)
          if (set [index] is T cast && condition (cast))
            action (cast);
      }
    }

    public void ForEach (Action<TElement> action)
    {
      for (var i = 0; i < setsList.Count; i++)
      {
        var set = setsList [i];

        for (var index = 0; index < set.Count; index++)
          action (set [index]);
      }
    }

    public void Where (Func<TElement, bool> condition, Action<TElement> action)
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

    public bool Any (Func<TElement, bool> condition)
    {
      for (var i = 0; i < setsList.Count; i++)
        if (setsList [i].Any (condition))
          return true;
      return false;
    }

    public virtual void Clear (Type keyType)
    {
      if (setsCache.TryGetValue (keyType, out Set<TElement> set))
      {
        set.ForEach (OnElementRemoved);
        set.Clear ();
      }
    }

    public virtual void Clear ()
    {
      for (var i = 0; i < setsList.Count; i++)
        setsList [i].Clear ();
    }

    public Set<TElement> GetOrCreate<TType> () => GetOrCreate (typeof(TType));

    public Set<TElement> GetOrCreate (Type type)
    {
      if (!setsCache.TryGetValue (type, out Set<TElement> set))
      {
        set = new Set<TElement> (this);
        setsList.Add (set);
        setsCache.Add (type, set);
      }

      return set;
    }

    public Set<TElement> Get<TType> ()
      => setsCache [typeof(TType)];

    public bool TryGet<TType> (out Set<TElement> set)
      => setsCache.TryGetValue (typeof(TType), out set);
  }
}