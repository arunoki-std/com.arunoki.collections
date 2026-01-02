using Arunoki.Collections.Utilities;

using System;

namespace Arunoki.Collections
{
  public abstract class ElementHandler<TElement> : IElementHandler<TElement>
  {
    private IElementHandler<TElement> targetHandler;

    protected ElementHandler (IElementHandler<TElement> targetHandler)
    {
      InitElementHandler (targetHandler);
    }

    protected void InitElementHandler (IElementHandler<TElement> targetHandler)
    {
      if (Utils.IsDebug ())
      {
        if (this.targetHandler != null && targetHandler != null)
          throw new InvalidOperationException (
            $"Trying to rewrite existing {nameof(IElementHandler<TElement>.TargetHandler)} '{this.targetHandler}' by '{targetHandler}'.");

        if (this.targetHandler == this)
          throw new InvalidOperationException (
            $"Can't add itself as {nameof(IElementHandler<TElement>.TargetHandler)}");
      }

      this.targetHandler = targetHandler;
    }

    IElementHandler<TElement> IElementHandler<TElement>.TargetHandler
    {
      get => targetHandler;
      set => InitElementHandler (value);
    }

    void IElementHandler<TElement>.OnElementAdded (TElement element) => OnElementAdded (element);

    void IElementHandler<TElement>.OnElementRemoved (TElement element) => OnElementRemoved (element);

    protected virtual void OnElementAdded (TElement element)
    {
      targetHandler?.OnElementAdded (element);
    }

    protected virtual void OnElementRemoved (TElement element)
    {
      targetHandler?.OnElementRemoved (element);
    }
  }
}