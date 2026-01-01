using System;

namespace Arunoki.Collections
{
  public partial class Set<TElement> : BaseSet<TElement>
  {
    public override void RemoveWhere (Func<TElement, bool> condition)
    {
      for (var index = Elements.Count - 1; index >= 0; index--)
        if (condition (Elements [index]))
          RemoveAt (index);
    }

    public override void ForEach (Action<TElement> action)
    {
      for (var index = Elements.Count - 1; index > -1 && index < Elements.Count; index--)
        action (Elements [index]);
    }

    public override void Cast<T> (Action<T> action)
    {
      for (var index = Elements.Count - 1; index > -1; index--)
        if (Elements [index] is T cast)
          action (cast);
    }

    public override void Cast<T> (Func<T, bool> condition, Action<T> action)
    {
      for (var index = Elements.Count - 1; index > -1; index--)
        if (Elements [index] is T cast && condition (cast))
          action (cast);
    }

    public override void Where (Func<TElement, bool> condition, Action<TElement> action)
    {
      for (var index = Elements.Count - 1; index > -1; index--)
      {
        var element = Elements [index];

        if (condition (element))
          action (element);
      }
    }

    public override void Clear ()
    {
      for (var index = Elements.Count - 1; index >= 0; index--)
        RemoveAt (index);

      Elements = new();
    }
  }
}