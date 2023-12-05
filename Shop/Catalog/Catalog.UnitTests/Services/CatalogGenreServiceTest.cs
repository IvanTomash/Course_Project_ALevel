using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services;
using Catalog.Host.Services.Interfaces;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;

namespace Catalog.UnitTests.Services;

public class CatalogGenreServiceTest
{
    private readonly ICatalogGenreService _catalogGenreService;

    private readonly Mock<ICatalogGenreRepository> _catalogGenreRepository;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;

    private readonly CatalogGenre _testGenre = new CatalogGenre()
    {
        Genre = "TestGenre"
    };

    public CatalogGenreServiceTest()
    {
        _catalogGenreRepository = new Mock<ICatalogGenreRepository>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogGenreService = new CatalogGenreService(_dbContextWrapper.Object, _logger.Object, _catalogGenreRepository.Object);
    }

    [Fact]
    public async Task Add_Success()
    {
        int? testResult = 1;

        _catalogGenreRepository.Setup(s => s.Add(
            It.IsAny<string>())).ReturnsAsync(testResult);

        var result = await _catalogGenreService.Add(_testGenre.Genre);

        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Add_Failed()
    {
        int? testResult = null;

        _catalogGenreRepository.Setup(s => s.Add(
            It.IsAny<string>())).ReturnsAsync(testResult);

        var result = await _catalogGenreService.Add(_testGenre.Genre);

        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Delete_Success()
    {
        int? testResult = 1;
        _catalogGenreRepository.Setup(s => s.Delete(It.IsAny<int>())).ReturnsAsync(testResult);

        var result = await _catalogGenreService.Delete(_testGenre.Id);
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Delete_Failed()
    {
        int? testResult = null;
        _catalogGenreRepository.Setup(s => s.Delete(It.IsAny<int>())).ReturnsAsync(testResult);

        var result = await _catalogGenreService.Delete(_testGenre.Id);
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Update_Success()
    {
        int? testResult = 1;

        _catalogGenreRepository.Setup(s => s.Update(
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        var result = await _catalogGenreService.Update(_testGenre.Id, _testGenre.Genre);

        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Update_Failed()
    {
        int? testResult = null;

        _catalogGenreRepository.Setup(s => s.Update(
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        var result = await _catalogGenreService.Update(_testGenre.Id, _testGenre.Genre);

        result.Should().Be(testResult);
    }
}
