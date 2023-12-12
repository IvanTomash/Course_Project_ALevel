using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services;
using Order.Host.Services.Interfaces;

namespace Order.UnitTests.Services;

public class OrderGameServiceTest
{
    private readonly IOrderGameService _orderGameService;

    private readonly Mock<IOrderGameRepository> _orderGameRepository;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<OrderService>> _logger;

    private readonly OrderGame _testGame = new OrderGame()
    {
        Id = 1,
        ProductId = 1,
        Name = "Test",
        Price = 1,
        Count = 1,
        OrderNumberId = 1,
    };

    public OrderGameServiceTest()
    {
        _orderGameRepository = new Mock<IOrderGameRepository>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<OrderService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _orderGameService = new OrderGameService(
            _dbContextWrapper.Object,
            _logger.Object,
            _orderGameRepository.Object);
    }

    [Fact]
    public async Task Add_Success()
    {
        int? testRes = 1;

        _orderGameRepository.Setup(s => s.Add(
            It.IsAny<int>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>())).ReturnsAsync(testRes);

        var result = await _orderGameService.Add(_testGame.Id, _testGame.Name, _testGame.Price, _testGame.Count, _testGame.OrderNumberId);

        result.Should().Be(testRes);
    }

    [Fact]
    public async Task Add_Failed()
    {
        _orderGameRepository.Setup(s => s.Add(
            It.IsAny<int>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>())).ReturnsAsync((Func<int?>)null!);

        var result = await _orderGameService.Add(_testGame.Id, _testGame.Name, _testGame.Price, _testGame.Count, _testGame.OrderNumberId);

        result.Should().BeNull();
    }

    [Fact]
    public async Task Delete_Success()
    {
        int? testRes = 1;

        _orderGameRepository.Setup(s => s.Delete(
            It.IsAny<int>())).ReturnsAsync(testRes);

        var result = await _orderGameService.Delete(_testGame.Id);

        result.Should().Be(testRes);
    }

    [Fact]
    public async Task Delete_Failed()
    {
        _orderGameRepository.Setup(s => s.Delete(
            It.IsAny<int>())).ReturnsAsync((Func<int?>)null!);

        var result = await _orderGameService.Delete(_testGame.Id);

        result.Should().BeNull();
    }

    [Fact]
    public async Task Update_Success()
    {
        int? testRes = 1;

        _orderGameRepository.Setup(s => s.Update(
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>())).ReturnsAsync(testRes);

        var result = await _orderGameService.Update(_testGame.Id, _testGame.ProductId, _testGame.Name, _testGame.Price, _testGame.Count, _testGame.OrderNumberId);

        result.Should().Be(testRes);
    }

    [Fact]
    public async Task Update_Failed()
    {
        _orderGameRepository.Setup(s => s.Update(
           It.IsAny<int>(),
           It.IsAny<int>(),
           It.IsAny<string>(),
           It.IsAny<decimal>(),
           It.IsAny<int>(),
           It.IsAny<int>())).ReturnsAsync((Func<int?>)null!);

        var result = await _orderGameService.Update(_testGame.Id, _testGame.ProductId, _testGame.Name, _testGame.Price, _testGame.Count, _testGame.OrderNumberId);

        result.Should().BeNull();
    }
}
