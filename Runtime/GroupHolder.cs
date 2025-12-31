using Arunoki.Collections.Utils;

using System;
using System.Collections.Generic;

namespace Arunoki.Collections
{
  public class GroupHolder<TElement> : BaseSet<TElement>
  {
    private readonly List<BaseSet<TElement>> groups = new();

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

      if (!groups.Contains (set))
      {
        groups.Insert (0, set);
      }
    }

    public virtual void ForEachSet<TSet> (Action<TSet> action)
    {
      for (var i = groups.Count - 1; i >= 0; i--)
        if (groups [i] is TSet set)
          action (set);
    }

    public virtual bool ForAnySet<TSet> (Func<TSet, bool> condition)
    {
      for (var i = groups.Count - 1; i >= 0; i--)
        if (groups [i] is TSet set && condition (set))
          return true;

      return false;
    }

    public override void Where (Func<TElement, bool> condition, Action<TElement> action)
    {
      for (var i = groups.Count - 1; i >= 0; i--)
        groups [i].Where (condition, action);
    }

    public override void Cast<T> (Action<T> action)
    {
      for (var i = groups.Count - 1; i >= 0; i--)
        groups [i].Cast (action);
    }

    public override void Cast<T> (Func<T, bool> condition, Action<T> action)
    {
      for (var i = groups.Count - 1; i >= 0; i--)
        groups [i].Cast (condition, action);
    }

    public override void ForEach (Action<TElement> action)
    {
      for (var i = groups.Count - 1; i >= 0; i--)
        groups [i].ForEach (action);
    }

    public override void Clear ()
    {
      groups.ForEach (collection => collection.Clear ());
    }
  }
}