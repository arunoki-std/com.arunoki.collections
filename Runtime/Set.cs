using System;
using System.Collections.Generic;

namespace Arunoki.Collections
{
  public partial class Set<TElement>
  {
    protected List<TElement> Elements = new();

    public Set ()
    {
    }

    public Set (ISetHandler<TElement> targetHandler) : base (targetHandler)
    {
    }

    public TElement this [int index] => Elements [(Elements.Count - 1) - index];

    public int Count => Elements.Count;

    public virtual void Add (TElement element)
    {
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
        Elements.RemoveAt (index);
        OnElementRemoved (Elements [index]);
        return true;
      }

      return false;
    }
  }
}