using System;
using System.Collections.Generic;

namespace Arunoki.Collections
{
  public class Set<TElement> : BaseSet<TElement>
  {
    protected List<TElement> Elements = new();

    public Set () : base ()
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

    protected void RemoveNulls ()
    {
      for (var i = Elements.Count - 1; i >= 0; i--)
        if (Elements [i] is null)
          Elements.RemoveAt (i);
    }

    public override IEnumerable<T> GetAll<T> ()
    {
      if (this is T self)
        yield return self;

      for (var i = Elements.Count - 1; i >= 0; i--)
        if (Elements [i] is T match)
          yield return match;
    }

    public override IEnumerable<TElement> GetAll ()
    {
      for (var i = Elements.Count - 1; i >= 0; i--)
        yield return Elements [i];
    }

    public override IEnumerable<TElement> Where (Predicate<TElement> condition)
    {
      for (var i = Elements.Count - 1; i >= 0; i--)
        if (condition (Elements [i]))
          yield return Elements [i];
    }

    public override IEnumerable<T> Where<T> (Predicate<T> condition)
    {
      for (var i = Elements.Count - 1; i >= 0; i--)
        if (Elements [i] is T match && condition (match))
          yield return match;
    }

    public override void ForEach (Action<TElement> action)
    {
      for (var i = Elements.Count - 1; i > -1 && i < Elements.Count; i--)
        action (Elements [i]);
    }

    public override void ForEach (Predicate<TElement> condition, Action<TElement> action)
    {
      for (var i = Elements.Count - 1; i >= 0; i--)
        if (condition (Elements [i]))
          action (Elements [i]);
    }

    public override void ForEachOf<T> (Predicate<T> condition, Action<T> action)
    {
      for (var i = Elements.Count - 1; i >= 0; i--)
        if (Elements [i] is T match && condition (match))
          action (match);
    }

    public override void Clear ()
    {
      for (var i = Elements.Count - 1; i >= 0; i--)
        RemoveAt (i);

      Elements = new();
    }
  }
}