using Order.Host.Data;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services.Interfaces;

namespace Order.Host.Services;

public class OrderNumberService : BaseDataService<ApplicationDbContext>, IOrderNumberService
{
    private readonly IOrderNumberRepository _orderNumberRepository;

    public OrderNumberService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        IOrderNumberRepository orderNumberRepository)
        : base(dbContextWrapper, logger)
    {
        _orderNumberRepository = orderNumberRepository;
    }

    public Task<int?> Add(string number)
    {
        return ExecuteSafeAsync(() => _orderNumberRepository.Add(number));
    }

    public Task<int?> Delete(int id)
    {
        return ExecuteSafeAsync(() => _orderNumberRepository.Delete(id));
    }

    public Task<int?> Update(int id, string number)
    {
       return ExecuteSafeAsync(() => _orderNumberRepository.Update(id, number));
    }
}
