using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.ViewModels.BasketViewModels;

public class IndexViewModel
{
    public IEnumerable<BasketGame> BasketGames { get; set; }
}