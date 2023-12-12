using Order.Host.Data.Entities;

namespace Order.Host.Repositories.Interfaces;

public interface IOrderNumberRepository
{
    Task<IEnumerable<OrderNumber>> GetOrderNumbers();
    Task<int?> Add(string number, string? personId);
    Task<int?> Delete(int id);
    Task<int?> Update(int id, string number);
}
