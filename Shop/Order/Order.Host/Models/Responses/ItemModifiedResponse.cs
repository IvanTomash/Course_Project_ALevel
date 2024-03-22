namespace Order.Host.Models.Responses;

public class ItemModifiedResponse<T>
{
    public T Id { get; set; } = default(T) !;
}
