namespace MVC.ViewModels;

public class OrderGame
{
    public int Id { get; set; }

    public int Productid { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int Count { get; set; }

    public OrderNumber OrderNumber { get; set; }

    public decimal TotalPrice
    {
        get
        {
            return Price * Count;
        }
    }
}
