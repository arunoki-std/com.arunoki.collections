using Arunoki.Collections.Utilities;

using System;
using System.Collections.Generic;

namespace Arunoki.Collections
{
  /// Ordered unique collection.
  /// Iteration order: insertion order (oldest to newest).
  /// Internal storage may differ to allow removing current element during iteration.
  public partial class Set<TElement> : Container<TElement>
  {
    /// Iteration order: insertion order (oldest to newest)
    protected List<TElement> Elements = new();

    public Set () : base (null) { }
    public Set (IContainer<TElement> targetContainer) : base (targetContainer) { }

    public TElement this [int index] => Elements [(Elements.Count - 1) - index];
    public int Count => Elements.Count;
    public bool Contains (TElement element) => Elements.Contains (element);

    public virtual void Add (TElement element)
    {
      if (Utils.IsDebug ())
      {
        if (element is null)
          throw new ArgumentNullException (nameof(element),
            $"Trying to add a null as element to the collection '{this}'.");

        if (Elements.Contains (element))
          throw new DuplicateElementException (element, this);
      }

      Elements.Insert (0, element);
      OnElementAdded (element);
    }

    public virtual bool TryAdd (TElement element)
    {
      if (Utils.IsDebug ())
      {
        if (element is null)
          throw new ArgumentNullException (nameof(element),
            $"Trying to add a null as element to the collection '{this}'.");
      }

      if (!Elements.Contains (element))
      {
        Elements.Insert (0, element);
        OnElementAdded (element);
        return true;
      }

      return false;
    }

    public virtual bool Remove (TElement element)
    {
      return RemoveAt (Elements.IndexOf (element));
    }

    public virtual bool RemoveAt (int index)
    {
      if (index > -1 && index < Elements.Count)
      {
        var element = Elements [index];
        Elements.RemoveAt (index);
        OnElementRemoved (element);
        return true;
      }

      return false;
    }
  }
}