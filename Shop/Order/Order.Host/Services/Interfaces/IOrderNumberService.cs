namespace Order.Host.Services.Interfaces;

public interface IOrderNumberService
{
    Task<int?> Add(string number);
    Task<int?> Delete(int id);
    Task<int?> Update(int id, string number);
}
