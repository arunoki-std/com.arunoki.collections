using System;
using System.Collections.Generic;
using System.Linq;

namespace Arunoki.Collections
{
  public class SetsTypeCollection<TElement> : Container<TElement>, ISet<TElement>
  {
    protected readonly Dictionary<Type, Set<TElement>> SetsCache = new(8);
    protected readonly List<Set<TElement>> SetsList = new(32);

    public SetsTypeCollection () : this (null) { }
    public SetsTypeCollection (IContainer<TElement> container) : base (container) { }

    public int Count
    {
      get
      {
        var count = 0;
        for (var i = 0; i < SetsList.Count; i++)
          count += SetsList [i].Count;
        return count;
      }
    }

    public void Add (Type keyType, params TElement [] elements)
    {
      if (elements == null || elements.Length == 0) return;

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
      for (int index = 0; index < SetsList.Count; index++)
        SetsList [index].RemoveWhere (condition);
    }

    public bool Remove (TElement element)
    {
      for (int index = 0; index < SetsList.Count; index++)
        if (SetsList [index].Remove (element))
          return true;

      return false;
    }

    public void ForEach (Predicate<TElement> condition, Action<TElement> action)
    {
      for (var i = 0; i < SetsList.Count; i++)
      {
        var set = SetsList [i];

        for (var index = 0; index < set.Count; index++)
          if (condition (set [index]))
            action (set [index]);
      }
    }

    public void Cast<T> (Action<T> action)
    {
      for (var i = 0; i < SetsList.Count; i++)
      {
        var set = SetsList [i];

        for (var index = 0; index < set.Count; index++)
          if (set [index] is T cast)
            action (cast);
      }
    }

    public void Cast<T> (Func<T, bool> condition, Action<T> action)
    {
      for (var i = 0; i < SetsList.Count; i++)
      {
        var set = SetsList [i];

        for (var index = 0; index < set.Count; index++)
          if (set [index] is T cast && condition (cast))
            action (cast);
      }
    }

    public void ForEach (Action<TElement> action)
    {
      for (var i = 0; i < SetsList.Count; i++)
      {
        var set = SetsList [i];

        for (var index = 0; index < set.Count; index++)
          action (set [index]);
      }
    }

    public void Where (Func<TElement, bool> condition, Action<TElement> action)
    {
      for (var i = 0; i < SetsList.Count; i++)
      {
        var set = SetsList [i];

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
      for (var i = 0; i < SetsList.Count; i++)
        if (SetsList [i].Any (condition))
          return true;
      return false;
    }

    public void Clear<T> () => Clear (typeof(T));

    public virtual void Clear (Type keyType)
    {
      if (SetsCache.TryGetValue (keyType, out Set<TElement> set))
      {
        set.ForEach (OnElementRemoved);
        set.Clear ();

        SetsCache.Remove (keyType);
        OnKeyRemoved (keyType);
      }
    }

    public virtual void Clear ()
    {
      foreach (var key in SetsCache.Keys.ToArray ())
        Clear (key);
    }

    public Set<TElement> GetOrCreate<TType> () => GetOrCreate (typeof(TType));

    public Set<TElement> GetOrCreate (Type type)
    {
      if (!SetsCache.TryGetValue (type, out Set<TElement> set))
      {
        set = new Set<TElement> (this);
        SetsList.Add (set);
        SetsCache.Add (type, set);
        OnKeyAdded (type);
      }

      return set;
    }

    /// To override.
    protected virtual void OnKeyAdded (Type keyType)
    {
    }

    /// To override.
    protected virtual void OnKeyRemoved (Type keyType)
    {
    }

    public Set<TElement> Get<TType> ()
      => SetsCache [typeof(TType)];

    public bool TryGet<TType> (out Set<TElement> set)
      => SetsCache.TryGetValue (typeof(TType), out set);
  }
}