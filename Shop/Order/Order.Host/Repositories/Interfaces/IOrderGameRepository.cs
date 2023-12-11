using Order.Host.Data.Entities;

namespace Order.Host.Repositories.Interfaces;

public interface IOrderGameRepository
{
    Task<IEnumerable<OrderGame>> GetOrderGames();
    Task<int?> Add(int productId, string name, decimal price, int count, int orderNumberId);
    Task<int?> Delete(int id);
    Task<int?> Update(int id, int productId, string name, decimal price, int count, int orderNumberId);
}
