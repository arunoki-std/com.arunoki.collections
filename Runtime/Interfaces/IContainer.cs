namespace Arunoki.Collections
{
  public interface IContainer<T>
  {
    IContainer<T> TargetContainer { get; set; }

    void OnElementAdded (T element);

    void OnElementRemoved (T element);
  }
}