using Arunoki.Collections.Utilities;

using System;
using System.Collections.Generic;

namespace Arunoki.Collections
{
  public partial class Set<TElement> : ElementHandler<TElement>
  {
    protected List<TElement> Elements = new();

    public Set () : base (null) { }
    public Set (IElementHandler<TElement> targetHandler) : base (targetHandler) { }

    public TElement this [int index] => Elements [(Elements.Count - 1) - index];

    public int Count => Elements.Count;

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