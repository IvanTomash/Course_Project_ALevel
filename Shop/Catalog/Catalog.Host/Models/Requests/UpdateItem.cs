namespace Catalog.Host.Models.Requests;

public class UpdateItem<T>
{
    public T Item { get; set; } = default(T) !;
}
