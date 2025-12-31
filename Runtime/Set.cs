using System;
using System.Collections.Generic;

namespace Arunoki.Collections
{
  public partial class Set<TElement> : BaseSet<TElement>
  {
    protected List<TElement> Elements = new();

    public Set ()
    {
    }

    public Set (ISetHandler<TElement> targetHandler) : base (targetHandler)
    {
    }

    public TElement this [int index] => Elements [index];

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
        OnElementRemoved (Elements [index]);
        Elements.RemoveAt (index);
        return true;
      }

      return false;
    }

    public virtual void RemoveWhere (Func<TElement, bool> condition)
    {
      for (var index = Elements.Count - 1; index >= 0; index--)
        if (condition (Elements [index]))
          RemoveAt (index);
    }

    protected void RemoveNulls ()
    {
      for (var index = Elements.Count - 1; index >= 0; index--)
        if (Elements [index] is null)
          Elements.RemoveAt (index);
    }

    public override void Clear ()
    {
      for (var index = Elements.Count - 1; index >= 0; index--)
        RemoveAt (index);

      Elements = new();
    }
  }
}