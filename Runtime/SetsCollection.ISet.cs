using System;

namespace Arunoki.Collections
{
  public partial class SetsCollection<TElement> : ISet<TElement>
  {
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