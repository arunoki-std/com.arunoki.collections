namespace Arunoki.Collections
{
  public interface ISetHandler<TElement>
  {
    ISetHandler<TElement> TargetSetHandler { get; set; }

    void OnElementAdded (TElement element);

    void OnElementRemoved (TElement element);
  }
}