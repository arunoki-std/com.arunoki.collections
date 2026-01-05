using System;

namespace Arunoki.Collections
{
  public interface ISet<out TElement>
  {
    int Count { get; }

    void RemoveWhere (Func<TElement, bool> condition);

    void ForEach (Action<TElement> action);

    void Cast<T> (Action<T> action);

    void Cast<T> (Func<T, bool> condition, Action<T> action);

    void Where (Func<TElement, bool> condition, Action<TElement> action);

    bool Any (Func<TElement, bool> condition);

    void Clear ();
  }
}