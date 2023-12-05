using MVC.ViewModels;

namespace MVC.Services.Interfaces;

public interface IBasketService
{
    Task<Basket> GetBasketGames();

    Task AddGameToBasket(int id, string name, decimal price);

    Task RemoveGameFromBasket(int id);
}
