namespace Arunoki.Collections
{
  public class Pair<TKey, TValue>
  {
    public readonly TKey Key;
    public readonly TValue Value;

    public Pair (TKey key, TValue value)
    {
      Key = key;
      Value = value;
    }

    public override string ToString ()
      => $"({Key}, {Value})";
  }
}