using System.Collections.Generic;

namespace Arunoki.Collections
{
  public partial class SetsCollection<TElement> : Container<TElement>
  {
    protected readonly List<ISet<TElement>> Sets = new();

    public SetsCollection (object setsSource = null) : this (null, setsSource)
    {
    }

    public SetsCollection (IContainer<TElement> rootContainer, object setsSource = null) : base (rootContainer)
    {
      if (setsSource != null)
      {
        AddSetsFrom (setsSource);
      }
    }
  }
}