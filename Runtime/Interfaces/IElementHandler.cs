namespace Arunoki.Collections
{
  public interface IElementHandler<TElement>
  {
    IElementHandler<TElement> TargetHandler { get; set; }

    void OnElementAdded (TElement element);

    void OnElementRemoved (TElement element);
  }
}