using Arunoki.Collections.Utils;

using System;
using System.Collections.Generic;

namespace Arunoki.Collections
{
  public class GroupSet<TElement> : BaseSet<TElement>
  {
    private readonly List<BaseSet<TElement>> sets = new();

    protected virtual void TrySetTargetHandler (object groupHandler)
    {
      if (groupHandler is ISetHandler<TElement> e)
        SetTargetHandler (e);
    }

    protected virtual void SetTargetHandler (ISetHandler<TElement> setHandler)
    {
      (this as ISetHandler<TElement>).TargetSetHandler = setHandler;
    }

    public virtual void FindSetsAt (object source)
    {
      foreach (var set in source.GetAllProperties<BaseSet<TElement>> ())
        AddSet (set);
    }

    public virtual void AddSet (BaseSet<TElement> set)
    {
      if (set == this) return;
      if (set is null) return;

      if (!sets.Contains (set))
        sets.Insert (0, set);
    }

    public virtual void ForEachSet<TSet> (Action<TSet> action)
    {
      for (var index = sets.Count - 1; index >= 0; index--)
        if (sets [index] is TSet set)
          action (set);
    }

    public virtual bool ForAnySet<TSet> (Func<TSet, bool> condition)
    {
      for (var index = sets.Count - 1; index >= 0; index--)
        if (sets [index] is TSet set && condition (set))
          return true;

      return false;
    }

    public override void RemoveWhere (Func<TElement, bool> condition)
    {
      for (var index = sets.Count - 1; index >= 0; index--)
        sets [index].RemoveWhere (condition);
    }

    /// For each element do <param name="action"></param> if it passes <param name="condition"></param>
    public override void Where (Func<TElement, bool> condition, Action<TElement> action)
    {
      for (var index = sets.Count - 1; index >= 0; index--)
        sets [index].Where (condition, action);
    }

    /// Cast elements at each set and for each element of type T do <param name="action"></param>
    public override void Cast<T> (Action<T> action)
    {
      for (var index = sets.Count - 1; index >= 0; index--)
        sets [index].Cast (action);
    }

    /// Cast elements at each set and for each element of type T do <param name="action"></param> if it passes <param name="condition"></param>
    public override void Cast<T> (Func<T, bool> condition, Action<T> action)
    {
      for (var index = sets.Count - 1; index >= 0; index--)
        sets [index].Cast (condition, action);
    }

    /// For each element at each set
    public override void ForEach (Action<TElement> action)
    {
      for (var index = sets.Count - 1; index >= 0; index--)
        sets [index].ForEach (action);
    }

    /// Clear elements at each set 
    public override void Clear ()
    {
      for (var index = sets.Count - 1; index >= 0; index--)
        sets [index].Clear ();
    }
  }
}