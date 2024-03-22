namespace Order.Host.Models.Requests;

public class AddGameRequest
{
    public int ProductId { get; set; }

    public string Name { get; init; } = default(string) !;

    public decimal Price { get; set; }

    public int Count { get; set; }

    public int OrderNumberId { get; set; }
}
