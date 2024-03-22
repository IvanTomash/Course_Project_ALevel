using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
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

public class CatalogGameServiceTest
{
    private readonly ICatalogGameService _catalogGameService;

    private readonly Mock<ICatalogGameRepository> _catalogGameRepository;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;

    private readonly CatalogGame _testGame = new CatalogGame()
    {
        Name = "TestName",
        Description = "TestDdescription",
        Price = 1000,
        AvailableStock = 100,
        PictureFileName = "test.png",
        CatalogGenreId = 1,
        CatalogPublisherId = 1,
    };

    public CatalogGameServiceTest()
    {
        _catalogGameRepository = new Mock<ICatalogGameRepository>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogGameService = new CatalogGameService(_dbContextWrapper.Object, _logger.Object, _catalogGameRepository.Object);
    }

    [Fact]
    public async Task Add_Success()
    {
        int? testResult = 1;

        _catalogGameRepository.Setup(s => s.Add(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        var result = await _catalogGameService.Add(_testGame.Name, _testGame.Description, _testGame.Price, _testGame.AvailableStock, _testGame.CatalogGenreId, _testGame.CatalogPublisherId, _testGame.PictureFileName);

        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Add_Failed()
    {
        int? testResult = null;

        _catalogGameRepository.Setup(s => s.Add(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        var result = await _catalogGameService.Add(_testGame.Name, _testGame.Description, _testGame.Price, _testGame.AvailableStock, _testGame.CatalogGenreId, _testGame.CatalogPublisherId, _testGame.PictureFileName);

        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Delete_Success()
    {
        int? testResult = 1;

        _catalogGameRepository.Setup(s => s.Delete(It.IsAny<int>())).ReturnsAsync(testResult);
        var result = await _catalogGameService.Delete(_testGame.Id);

        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Delete_Failed()
    {
        int? testResult = null;

        _catalogGameRepository.Setup(s => s.Delete(It.IsAny<int>())).ReturnsAsync(testResult);
        var result = await _catalogGameService.Delete(_testGame.Id);

        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Update_Success()
    {
        int? testResult = 1;

        _catalogGameRepository.Setup(s => s.Update(
            It.IsAny<int>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        var result = await _catalogGameService.Update(_testGame.Id, _testGame.Name, _testGame.Description, _testGame.Price, _testGame.AvailableStock, _testGame.CatalogGenreId, _testGame.CatalogPublisherId, _testGame.PictureFileName);

        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Update_Failed()
    {
        int? testResult = null;

        _catalogGameRepository.Setup(s => s.Update(
            It.IsAny<int>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        var result = await _catalogGameService.Update(_testGame.Id, _testGame.Name, _testGame.Description, _testGame.Price, _testGame.AvailableStock, _testGame.CatalogGenreId, _testGame.CatalogPublisherId, _testGame.PictureFileName);

        result.Should().Be(testResult);
    }
}
