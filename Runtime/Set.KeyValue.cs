using System.Collections.Generic;

namespace Arunoki.Collections
{
  public partial class Set<TKey, TElement>
  {
    /// Reversed list.
    protected List<Pair<TKey, TElement>> Elements = new();

    protected Dictionary<TKey, TElement> ElementsByKey = new();

    public TElement this [TKey key] => ElementsByKey [key];

    public bool Contains (TKey key)
      => ElementsByKey.ContainsKey (key);

    public bool TryGet (TKey key, out TElement value)
      => ElementsByKey.TryGetValue (key, out value);

    public virtual void Add (TKey key, TElement element)
    {
      Elements.Insert (0, new Pair<TKey, TElement> (key, element));
      ElementsByKey.Add (key, element);

      OnElementAdded (element);
    }

    public virtual bool RemoveAt (int index)
    {
      if (index > -1 && index < Elements.Count)
      {
        var pair = Elements [index];

        Elements.RemoveAt (index);
        ElementsByKey.Remove (pair.Key);

        OnElementRemoved (pair.Value);
        return true;
      }

      return false;
    }
  }
}