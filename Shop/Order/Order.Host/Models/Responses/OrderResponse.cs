using Order.Host.Models.Dtos;

namespace Order.Host.Models.Responses;

public class OrderResponse
{
    public IEnumerable<OrderNumberDto> OrderNumbers { get; init; } = null!;

    public IEnumerable<OrderGameDto> OrderGames { get; init; } = null!;
}
