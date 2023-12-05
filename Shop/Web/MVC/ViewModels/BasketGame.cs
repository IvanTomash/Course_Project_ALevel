namespace MVC.ViewModels;

public class BasketGame
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int Count { get; set; }

    public decimal TotalPrice 
    {        
        get
        {
            return Price * Count;
        } 
    }
}
