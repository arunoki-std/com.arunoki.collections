using System;

namespace Arunoki.Collections
{
  public abstract class BaseSet<TElement> : ISet<TElement>
  {
    private ISetHandler<TElement> targetSetHandler;
    private readonly Type elementType = typeof(TElement);

    protected BaseSet ()
    {
    }

    protected BaseSet (ISetHandler<TElement> targetSetHandler)
    {
      this.targetSetHandler = targetSetHandler;
    }

    public abstract void Cast<T> (Action<T> action);

    public abstract void Cast<T> (Func<T, bool> condition, Action<T> action);

    public abstract void ForEach (Action<TElement> action);

    public abstract void Where (Func<TElement, bool> condition, Action<TElement> action);

    public bool IsElement<T> () => typeof(T) == elementType;

    public abstract void Clear ();

    ISetHandler<TElement> ISetHandler<TElement>.TargetSetHandler
    {
      get => targetSetHandler;
      set => targetSetHandler = value;
    }

    void ISetHandler<TElement>.OnElementAdded (TElement element) => OnElementAdded (element);

    void ISetHandler<TElement>.OnElementRemoved (TElement element) => OnElementRemoved (element);

    protected virtual void OnElementAdded (TElement element)
    {
      targetSetHandler?.OnElementAdded (element);
    }

    protected virtual void OnElementRemoved (TElement element)
    {
      targetSetHandler?.OnElementRemoved (element);
    }
  }
}