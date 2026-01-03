using System.Collections.Generic;

namespace Arunoki.Collections
{
  public partial class SetsCollection<TElement> : Container<TElement>
  {
    private readonly List<ISet<TElement>> sets = new();

    public SetsCollection (object setsSource = null) : this (null, setsSource)
    {
    }

    public SetsCollection (IContainer<TElement> targetContainer, object setsSource = null) : base (targetContainer)
    {
      if (setsSource != null)
      {
        AddSetsFrom (setsSource);
      }
    }
  }
}