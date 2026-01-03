using Arunoki.Collections.Utilities;

using System;

namespace Arunoki.Collections
{
  public abstract class Container<TElement> : IContainer<TElement>
  {
    private IContainer<TElement> targetContainer;

    protected Container (IContainer<TElement> targetContainer)
    {
      SetTargetContainer (targetContainer);
    }

    protected void SetTargetContainer (IContainer<TElement> targetContainer)
    {
      if (Utils.IsDebug ())
      {
        if (this.targetContainer != null && targetContainer != null)
          throw new InvalidOperationException (
            $"Trying to rewrite existing {nameof(this.targetContainer)} '{this.targetContainer}' by '{targetContainer}'.");

        if (targetContainer == this)
          throw new InvalidOperationException (
            $"Can't add itself as {nameof(this.targetContainer)}");
      }

      this.targetContainer = targetContainer;
    }

    IContainer<TElement> IContainer<TElement>.TargetContainer
    {
      get => targetContainer;
      set => SetTargetContainer (value);
    }

    void IContainer<TElement>.OnElementAdded (TElement element) => OnElementAdded (element);

    void IContainer<TElement>.OnElementRemoved (TElement element) => OnElementRemoved (element);

    protected virtual void OnElementAdded (TElement element)
    {
      targetContainer?.OnElementAdded (element);
    }

    protected virtual void OnElementRemoved (TElement element)
    {
      targetContainer?.OnElementRemoved (element);
    }
  }
}