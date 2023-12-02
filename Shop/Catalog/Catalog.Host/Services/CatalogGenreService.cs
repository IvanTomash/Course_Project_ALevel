using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogGenreService : BaseDataService<ApplicationDbContext>, ICatalogGenreService
{
    private readonly ICatalogGenreRepository _catalogGenreRepository;

    public CatalogGenreService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogGenreRepository catalogGenreRepository)
        : base(dbContextWrapper, logger)
    {
        _catalogGenreRepository = catalogGenreRepository;
    }

    public Task<int?> Add(string genre)
    {
        return ExecuteSafeAsync(() => _catalogGenreRepository.Add(genre));
    }

    public Task<int?> Delete(int id)
    {
        return ExecuteSafeAsync(() => _catalogGenreRepository.Delete(id));
    }

    public Task<int?> Update(int id, string genre)
    {
        return ExecuteSafeAsync(() => _catalogGenreRepository.Update(id, genre));
    }
}
