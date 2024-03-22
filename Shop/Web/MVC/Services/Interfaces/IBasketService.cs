using MVC.ViewModels;

namespace MVC.Services.Interfaces;

public interface IBasketService
{
    Task<Basket> GetBasketGames();

    Task<int?> AddGameToBasket(int id, string name, decimal price);

    Task<int?> RemoveGameFromBasket(int id);

    Task ClearBasket();
}
