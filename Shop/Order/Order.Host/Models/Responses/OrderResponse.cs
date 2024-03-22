using Order.Host.Models.Dtos;

namespace Order.Host.Models.Responses;

public class OrderResponse
{
    public IEnumerable<OrderNumberDto> OrderNumbers { get; set; } = null!;

    public IEnumerable<OrderGameDto> OrderGames { get; set; } = null!;
}
