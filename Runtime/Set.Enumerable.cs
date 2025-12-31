using Arunoki.Collections.Enumerators;

using System;
using System.Collections;
using System.Collections.Generic;

namespace Arunoki.Collections
{
  public partial class Set<TElement> : IEnumerable<TElement>
  {
    public MutableCastEnumerable<TElement, T> Cast<T> () => new(Elements);

    public MutableWhereCastEnumerable<TElement, T> Cast<T> (Func<T, bool> condition) => new(Elements, condition);

    public override void Cast<T> (Action<T> action)
    {
      for (var i = Elements.Count - 1; i > -1; i--)
        if (Elements [i] is T cast)
          action (cast);
    }

    public override void Cast<T> (Func<T, bool> predicate, Action<T> action)
    {
      for (var i = Elements.Count - 1; i > -1; i--)
        if (Elements [i] is T cast && predicate (cast))
          action (cast);
    }

    public MutableWhereEnumerable<TElement> Where (Func<TElement, bool> condition) => new(Elements, condition);

    public override void Where (Func<TElement, bool> condition, Action<TElement> action)
    {
      for (var i = Elements.Count - 1; i > -1; i--)
      {
        var element = Elements [i];
        if (condition (element)) action (element);
      }
    }

    public override void ForEach (Action<TElement> action)
    {
      for (var i = Elements.Count - 1; i > -1 && i < Elements.Count; i--)
        action (Elements [i]);
    }

    public MutableIndexedEnumerable<TElement> WithIndex () => new(Elements);

    public MutableEnumerator<TElement> GetEnumerator () => new(Elements);

    IEnumerator<TElement> IEnumerable<TElement>.GetEnumerator () => new MutableEnumerator<TElement> (Elements);

    IEnumerator IEnumerable.GetEnumerator () => new MutableEnumerator<TElement> (Elements);
  }
}