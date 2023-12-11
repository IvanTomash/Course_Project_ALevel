namespace Order.Host.Models.Requests;

public class DeleteItemRequest<T>
{
    public T Id { get; set; } = default(T) !;
}
