namespace MVC.ViewModels.OrderViewModels;

public class IndexViewModel
{
    public IEnumerable<OrderNumber> OrderNumbers { get; set; }
    public IEnumerable<OrderGame> OrderGames { get; set; }

    public decimal PriceForOrder { get; set; }
}
