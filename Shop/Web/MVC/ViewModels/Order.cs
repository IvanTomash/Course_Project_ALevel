namespace MVC.ViewModels;

public class Order
{
    public IEnumerable<OrderNumber> OrderNumbers { get; set; }

    public IEnumerable<OrderGame> OrderGames { get; set; }
}
