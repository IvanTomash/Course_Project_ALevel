using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogPublisherService : BaseDataService<ApplicationDbContext>, ICatalogPublisherService
{
    private readonly ICatalogPublisherRepository _catalogPublisherRepository;

    public CatalogPublisherService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogPublisherRepository catalogPublisherRepository)
            : base(dbContextWrapper, logger)
    {
        _catalogPublisherRepository = catalogPublisherRepository;
    }

    public Task<int?> Add(string publisher)
    {
        return ExecuteSafeAsync(() => _catalogPublisherRepository.Add(publisher));
    }

    public Task<int?> Delete(int id)
    {
        return ExecuteSafeAsync(() => _catalogPublisherRepository.Delete(id));
    }

    public Task<int?> Update(int id, string publisher)
    {
        return ExecuteSafeAsync(() => _catalogPublisherRepository.Update(id, publisher));
    }
}
