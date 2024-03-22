#pragma warning disable CS8618
namespace Order.Host.Data.Entities;

public class OrderGame
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public int Count { get; set; }

    public int OrderNumberId { get; set; }

    public OrderNumber OrderNumber { get; set; }
}
