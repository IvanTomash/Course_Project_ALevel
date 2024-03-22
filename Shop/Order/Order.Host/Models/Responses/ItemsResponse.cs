namespace Order.Host.Models.Responses;

public class ItemsResponse<T>
{
    public long Count { get; set; }

    public IEnumerable<T> Data { get; init; } = null!;
}
