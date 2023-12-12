#pragma warning disable CS8618
namespace Order.Host.Models.Dtos;

public class OrderNumberDto
{
    public int Id { get; set; }
    public string Number { get; set; }
    public string PersonId { get; set; }
}
