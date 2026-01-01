using Arunoki.Collections.Enumerators;

using System.Collections;
using System.Collections.Generic;

namespace Arunoki.Collections
{
  public partial class Set<TKey, TElement> : IEnumerable<TElement>
  {
    /// var (index, key, value)
    public MutablePairWithIndex<TKey, TElement> WithIndex () => new(Elements);

    /// var (key, value)
    public MutablePair<TKey, TElement> WithKey () => new(Elements);

    public MutablePairValueEnumerator<TKey, TElement> GetEnumerator () => new(Elements);

    IEnumerator<TElement> IEnumerable<TElement>.GetEnumerator () => new MutablePairValueEnumerator<TKey, TElement> (Elements);

    IEnumerator IEnumerable.GetEnumerator () => new MutablePairValueEnumerator<TKey, TElement> (Elements);
  }
}