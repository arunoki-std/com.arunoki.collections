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

    public virtual void AddGroupsFrom (object source)
    {
      foreach (var group in source.GetAllProperties<BaseSet<TElement>> ())
        AddGroup (group);
    }

    public virtual void AddGroup (BaseSet<TElement> sets)
    {
      if (!groups.Contains (sets))
      {
        groups.Insert (0, sets);
      }
    }

    public virtual void ForEachGroup<TGroup> (Action<TGroup> action)
    {
      for (var i = groups.Count - 1; i >= 0; i--)
        if (groups [i] is TGroup group)
          action (group);
    }

    public virtual IEnumerable<TGroup> ForEachGroup<TGroup> ()
    {
      for (var i = groups.Count - 1; i >= 0; i--)
        if (groups [i] is TGroup group)
          yield return group;
    }

    public virtual void SelectGroup<TGroup> (Predicate<TGroup> condition, Action<TGroup> action)
    {
      for (var i = groups.Count - 1; i >= 0; i--)
        if (groups [i] is TGroup group && condition (group))
          action (group);
    }

    public override IEnumerable<T> GetAll<T> ()
    {
      for (var i = groups.Count - 1; i >= 0; i--)
        foreach (T element in groups [i].GetAll<T> ())
          yield return element;
    }

    public override IEnumerable<TElement> GetAll ()
    {
      for (var i = groups.Count - 1; i >= 0; i--)
        foreach (TElement element in groups [i].GetAll<TElement> ())
          yield return element;
    }

    public override IEnumerable<T> Where<T> (Predicate<T> condition)
    {
      for (var i = groups.Count - 1; i >= 0; i--)
        foreach (T element in groups [i].Where (condition))
          yield return element;
    }

    public override IEnumerable<TElement> Where (Predicate<TElement> condition)
    {
      for (var i = groups.Count - 1; i >= 0; i--)
        foreach (TElement element in groups [i].Where (condition))
          yield return element;
    }

    public override void ForEach (Predicate<TElement> condition, Action<TElement> action)
    {
      for (var i = groups.Count - 1; i >= 0; i--)
        groups [i].ForEach (condition, action);
    }

    public override void ForEachOf<T> (Predicate<T> condition, Action<T> action)
    {
      for (var i = groups.Count - 1; i >= 0; i--)
        groups [i].ForEachOf (condition, action);
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