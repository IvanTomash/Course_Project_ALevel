using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogGameService : BaseDataService<ApplicationDbContext>, ICatalogGameService
{
    private readonly ICatalogGameRepository _catalogGameRepository;

    public CatalogGameService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogGameRepository catalogGameRepository)
        : base(dbContextWrapper, logger)
    {
        _catalogGameRepository = catalogGameRepository;
    }

    public Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogGenreId, int catalogPublisherId, string pictureFileName)
    {
        return ExecuteSafeAsync(() => _catalogGameRepository.Add(name, description, price, availableStock, catalogGenreId, catalogPublisherId, pictureFileName));
    }

    public Task<int?> Delete(int id)
    {
        return ExecuteSafeAsync(() => _catalogGameRepository.Delete(id));
    }

    public Task<int?> Update(int id, string name, string description, decimal price, int availableStock, int catalogGenreId, int catalogPublisherId, string pictureFileName)
    {
        return ExecuteSafeAsync(() => _catalogGameRepository.Update(id, name, description, price, availableStock, catalogGenreId, catalogPublisherId, pictureFileName));
    }
}