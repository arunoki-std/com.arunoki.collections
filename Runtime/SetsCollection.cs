using System.Collections.Generic;

namespace Arunoki.Collections
{
  public partial class SetsCollection<TElement> : ElementHandler<TElement>
  {
    private readonly List<ISet<TElement>> sets = new();

    public SetsCollection (object setsSource = null) : this (null, setsSource)
    {
    }

    public SetsCollection (IElementHandler<TElement> targetHandler, object setsSource = null) : base (targetHandler)
    {
      if (setsSource != null)
      {
        AddSetsFrom (setsSource);
      }
    }
  }
}