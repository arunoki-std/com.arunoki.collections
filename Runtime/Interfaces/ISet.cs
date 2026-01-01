using System;

namespace Arunoki.Collections
{
  public interface ISet<TElement> : ISetHandler<TElement>
  {
    void RemoveWhere (Func<TElement, bool> condition);

    void ForEach (Action<TElement> action);

    void Cast<T> (Action<T> action);

    void Cast<T> (Func<T, bool> condition, Action<T> action);

    void Where (Func<TElement, bool> condition, Action<TElement> action);

    void Clear ();
  }
}