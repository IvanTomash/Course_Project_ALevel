using System.ComponentModel.DataAnnotations;
using Order.Host.Models.Dtos;

namespace Order.Host.Models.Requests;

public class CreateOrderRequest
{
    [Required]
    public IEnumerable<RequestedProduct> Data { get; set; } = null!;
}
