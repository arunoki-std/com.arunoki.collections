using Arunoki.Collections.Utilities;

using System;

namespace Arunoki.Collections
{
  public partial class SetsCollection<TElement>
  {
    public void AddSetsFrom (object source)
    {
      var setList = source.GetAllProperties<ISet<TElement>> ();

      for (var index = 0; index < setList.Count; index++)
        TryAddSet (setList [index]);
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

      OnAddSet (set);
    }

    public bool TryAddSet (ISet<TElement> set)
    {
      if (set is null) return false;
      if (set == this) return false;
      if (!sets.Contains (set))
      {
        OnAddSet (set);
        return true;
      }

      return false;
    }

    protected virtual void OnAddSet (ISet<TElement> set)
    {
      sets.Add (set);
      (set as IContainer<TElement>).TargetContainer = this;
    }
  }
}