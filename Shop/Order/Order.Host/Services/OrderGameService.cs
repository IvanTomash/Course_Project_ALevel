using Order.Host.Data;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services.Interfaces;

namespace Order.Host.Services;

public class OrderGameService : BaseDataService<ApplicationDbContext>, IOrderGameService
{
    private readonly IOrderGameRepository _orderGameRepository;

    public OrderGameService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        IOrderGameRepository orderGameRepository)
        : base(dbContextWrapper, logger)
    {
        _orderGameRepository = orderGameRepository;
    }

    public Task<int?> Add(int productId, string name, decimal price, int count, int orderNumberId)
    {
        return ExecuteSafeAsync(() => _orderGameRepository.Add(productId, name, price, count, orderNumberId));
    }

    public Task<int?> Delete(int id)
    {
        return ExecuteSafeAsync(() => _orderGameRepository.Delete(id));
    }

    public Task<int?> Update(int id, int productId, string name, decimal price, int count, int orderNumberId)
    {
        return ExecuteSafeAsync(() => _orderGameRepository.Update(id, productId, name, price, count, orderNumberId));
    }
}
