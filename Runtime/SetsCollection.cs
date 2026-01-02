using Arunoki.Collections.Utilities;

using System;
using System.Collections.Generic;

namespace Arunoki.Collections
{
  public class SetsCollection<TElement> : ElementHandler<TElement>, ISet<TElement>
  {
    private readonly List<ISet<TElement>> sets = new();

    public SetsCollection (object setsSource = null) : this (null, setsSource)
    {
    }

    public SetsCollection (IElementHandler<TElement> targetHandler, object setsSource = null) : base (targetHandler)
    {
      if (setsSource != null)
        AddSetsFrom (setsSource);
    }

    public virtual void AddSetsFrom (object source)
    {
      foreach (var set in source.GetAllProperties<ISet<TElement>> ())
      {
        if (set == this) continue;
        if (sets.Contains (set)) continue;

        AddSet (set);
      }
    }

    public virtual void AddSet (ISet<TElement> set)
    {
      if (Utils.IsDebug ())
      {
        if (set is null)
          throw new ArgumentNullException ($"Trying to add null to the collection of sets '{this}'.");

        if (set == this)
          throw new InvalidOperationException ($"Trying to add itself to the collection of sets '{this}'.");

        if (sets.Contains (set))
          throw new DuplicateElementException ($"Set '{set}' already exists in the collection of sets '{this}'.");
      }

      sets.Insert (0, set);
    }

    public void ForEachSet<TSet> (Action<TSet> action)
    {
      for (var index = 0; index < sets.Count; index++)
        if (sets [index] is TSet set)
          action (set);
    }

    public bool ForAnySet<TSet> (Func<TSet, bool> condition)
    {
      for (var index = 0; index < sets.Count; index++)
        if (sets [index] is TSet set && condition (set))
          return true;

      return false;
    }

    public void RemoveWhere (Func<TElement, bool> condition)
    {
      for (var index = 0; index < sets.Count; index++)
        sets [index].RemoveWhere (condition);
    }

    /// For each element do <param name="action"></param> if it passes <param name="condition"></param>
    public void Where (Func<TElement, bool> condition, Action<TElement> action)
    {
      for (var index = 0; index < sets.Count; index++)
        sets [index].Where (condition, action);
    }

    /// Cast elements at each set and for each element of type T do <param name="action"></param>
    public void Cast<T> (Action<T> action)
    {
      for (var index = 0; index < sets.Count; index++)
        sets [index].Cast (action);
    }

    /// Cast elements at each set and for each element of type T do <param name="action"></param> if it passes <param name="condition"></param>
    public void Cast<T> (Func<T, bool> condition, Action<T> action)
    {
      for (var index = 0; index < sets.Count; index++)
        sets [index].Cast (condition, action);
    }

    /// For each element at each set
    public void ForEach (Action<TElement> action)
    {
      for (var index = 0; index < sets.Count; index++)
        sets [index].ForEach (action);
    }

    /// Clear elements at each set 
    public virtual void Clear ()
    {
      for (var index = 0; index < sets.Count; index++)
        sets [index].Clear ();
    }
  }
}