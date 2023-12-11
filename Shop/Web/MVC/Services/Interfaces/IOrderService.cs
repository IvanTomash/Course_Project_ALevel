using MVC.ViewModels;

namespace MVC.Services.Interfaces;

public interface IOrderService
{
    Task<Order> GetOrders();

    Task<int?> CreateOrder();
}