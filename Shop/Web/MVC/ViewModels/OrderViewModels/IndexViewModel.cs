namespace MVC.ViewModels.OrderViewModels;

public class IndexViewModel
{
    public IEnumerable<OrderNumber> OrderNumbers { get; set; }
    public IEnumerable<OrderGame> OrderGames { get; set; }

    public decimal Price 
    {
        get
        {
            decimal totalPrice = 0;
            foreach (var game in OrderGames)
            {
                totalPrice += game.TotalPrice;
            }

            return totalPrice;
        }
    }
}
