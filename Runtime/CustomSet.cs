using System;

namespace Arunoki.Collections
{
  public abstract class CustomSet<TElement> : Container<TElement>, ISet<TElement>
  {
    protected CustomSet (IContainer<TElement> targetContainer = null) : base (targetContainer)
    {
    }

    public int Count => GetSet ().Count;

    /// Concrete set.
    protected abstract ISet<TElement> GetSet ();
    
    public bool Contains (TElement item) => GetSet ().Contains (item);

    public void RemoveWhere (Func<TElement, bool> condition)
      => GetSet ().RemoveWhere (condition);

    public void ForEach (Action<TElement> action)
      => GetSet ().ForEach (action);

    public void Cast<T> (Action<T> action)
      => GetSet ().Cast (action);

    public void Cast<T> (Func<T, bool> condition, Action<T> action)
      => GetSet ().Cast (condition, action);

    public void Where (Func<TElement, bool> condition, Action<TElement> action)
      => GetSet ().Where (condition, action);

    public bool Any (Func<TElement, bool> condition)
      => GetSet ().Any (condition);

    /// Clear all elements.
    public virtual void Clear ()
    {
      GetSet ().Clear ();
    }
  }
}