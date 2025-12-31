using System.Collections;
using System.Collections.Generic;

namespace Arunoki.Collections.Enumerators
{
  /// Enumerator is reversed
  public struct MutableEnumerator<T> : IEnumerator<T>
  {
    private readonly List<T> list;
    private int index;

    public MutableEnumerator (List<T> list)
    {
      this.list = list;
      index = list.Count;
    }

    public T Current => list [index];
    object IEnumerator.Current => Current!;
    public bool MoveNext () => --index > -1;
    public void Reset () => index = list.Count;
    public void Dispose () { }
  }
}