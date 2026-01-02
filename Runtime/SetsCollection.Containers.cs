using Arunoki.Collections.Utilities;

using System;

namespace Arunoki.Collections
{
  public partial class SetsCollection<TElement>
  {
    public void AddSetsFrom (object source)
    {
      foreach (var set in source.GetAllProperties<ISet<TElement>> ())
      {
        TryAddSet (set);
      }
    }

    public void AddSet (ISet<TElement> set)
    {
      if (Utils.IsDebug ())
      {
        if (set is null)
          throw new ArgumentNullException (nameof(set), $"Trying to add null to the collection of sets '{this}'.");

        if (set == this)
          throw new InvalidOperationException ($"Trying to add itself to the collection of sets '{this}'.");

        if (sets.Contains (set))
          throw new DuplicateElementException ($"Set '{set}' already exists in the collection of sets '{this}'.");
      }

      sets.Add (set);
    }

    public bool TryAddSet (ISet<TElement> set)
    {
      if (set is null) return false;
      if (set == this) return false;
      if (!sets.Contains (set))
      {
        sets.Add (set);
        return true;
      }

      return false;
    }
  }
}