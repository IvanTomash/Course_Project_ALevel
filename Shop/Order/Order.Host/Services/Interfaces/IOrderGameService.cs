namespace Order.Host.Services.Interfaces;

public interface IOrderGameService
{
    Task<int?> Add(int productId, string name, decimal price, int count, int orderNumberId);
    Task<int?> Delete(int id);
    Task<int?> Update(int id, int productId, string name, decimal price, int count, int orderNumberId);
}
