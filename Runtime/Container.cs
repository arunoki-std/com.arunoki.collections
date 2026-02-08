using Arunoki.Collections.Utilities;

using System;

namespace Arunoki.Collections
{
  public abstract class Container<TElement> : IContainer<TElement>
  {
    private IContainer<TElement> rootContainer;

    protected Container (IContainer<TElement> rootContainer)
    {
      SetRootContainer (rootContainer);
    }

    protected void SetRootContainer (IContainer<TElement> targetContainer)
    {
      if (Utils.IsDebug ())
      {
        if (this.rootContainer != null && targetContainer != null)
          throw new InvalidOperationException (
            $"Trying to rewrite existing {nameof(this.rootContainer)} '{this.rootContainer}' by '{targetContainer}'.");

        if (targetContainer == this)
          throw new InvalidOperationException (
            $"Can't add itself as {nameof(this.rootContainer)}");
      }

      this.rootContainer = targetContainer;
    }

    IContainer<TElement> IContainer<TElement>.RootContainer
    {
      get => rootContainer;
      set => SetRootContainer (value);
    }

    void IContainer<TElement>.OnAdded (TElement element) => OnElementAdded (element);

    void IContainer<TElement>.OnRemoved (TElement element) => OnElementRemoved (element);

    protected virtual void OnElementAdded (TElement element)
    {
      rootContainer?.OnAdded (element);
    }

    protected virtual void OnElementRemoved (TElement element)
    {
      rootContainer?.OnRemoved (element);
    }
  }
}