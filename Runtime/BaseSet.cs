using System;
using System.Collections.Generic;

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

    public void ForEach<T> (Action<T> action)
    {
      foreach (var element in GetAll<T> ())
        action (element);
    }

    public abstract IEnumerable<T> GetAll<T> ();

    public abstract IEnumerable<TElement> GetAll ();

    public abstract void ForEach (Action<TElement> action);
    
    public abstract IEnumerable<T> Where<T> (Predicate<T> condition);

    public abstract IEnumerable<TElement> Where (Predicate<TElement> condition);

    public abstract void ForEach (Predicate<TElement> condition, Action<TElement> action);

    public abstract void ForEachOf<T> (Predicate<T> condition, Action<T> action);

    

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