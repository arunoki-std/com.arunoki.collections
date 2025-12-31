using System.Collections;
using System.Collections.Generic;

namespace Arunoki.Collections
{
  public struct Enumerator<T> : IEnumerator<T>
  {
    private List<T> list;
    private int index;

    public Enumerator (List<T> list)
    {
      this.list = list;
      this.index = -1;
    }

    public T Current => list [index];

    object IEnumerator.Current => Current!;

    public bool MoveNext () => ++index < list.Count;

    public void Reset () => index = -1;

    public void Dispose () => list = null;
  }
}