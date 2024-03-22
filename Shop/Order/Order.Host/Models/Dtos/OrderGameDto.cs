#pragma warning disable CS8618
namespace Order.Host.Models.Dtos;

public class OrderGameDto
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public int Count { get; set; }

    public OrderNumberDto OrderNumber { get; set; }
}
