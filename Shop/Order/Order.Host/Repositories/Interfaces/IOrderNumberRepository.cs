using Order.Host.Data.Entities;

namespace Order.Host.Repositories.Interfaces;

public interface IOrderNumberRepository
{
    Task<IEnumerable<OrderNumber>> GetOrderNumbers();
    Task<int?> Add(string number);
    Task<int?> Delete(int id);
    Task<int?> Update(int id, string number);
}
