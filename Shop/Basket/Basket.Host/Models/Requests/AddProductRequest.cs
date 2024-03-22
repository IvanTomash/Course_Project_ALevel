namespace Basket.Host.Models.Requests;

public class AddProductRequest
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int Count { get; set; }
}
