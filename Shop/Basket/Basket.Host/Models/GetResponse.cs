using Basket.Host.Models.Requests;

namespace Basket.Host.Models;

public class GetResponse
{
    public List<AddProductRequest> Data { get; set; } = null!;
}