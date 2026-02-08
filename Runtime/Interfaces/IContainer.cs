namespace Arunoki.Collections
{
  public interface IContainer<T>
  {
    IContainer<T> RootContainer { get; set; }

    void OnAdded (T element);

    void OnRemoved (T element);
  }
}