namespace Order.Host.Models.Requests;

public class RequestedProduct
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int Count { get; set; }
}
