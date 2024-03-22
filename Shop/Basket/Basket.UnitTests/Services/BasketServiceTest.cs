using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basket.Host.Models.Requests;
using Basket.Host.Services;
using Basket.Host.Services.Interfaces;
using FluentAssertions;
using Moq;

namespace Basket.UnitTests.Services;

public class BasketServiceTest
{
    private readonly IBasketService _basketService;

    private readonly Mock<ICacheService> _cacheService;

    public BasketServiceTest()
    {
        _cacheService = new Mock<ICacheService>();
        _basketService = new BasketService(_cacheService.Object);
    }

    [Fact]
    public async Task Get_Success()
    {
        string userId = "TestID";
        var listAddProductsTest = new List<AddProductRequest>
        {
            new AddProductRequest()
            {
                Id = 1,
                Name = "Test",
                Count = 1,
                Price = 1
            }
        };
        _cacheService.Setup(s => s.GetAsync<List<AddProductRequest>>(
            It.Is<string>(i => i == userId))).ReturnsAsync(listAddProductsTest);

        var result = await _basketService.Get(userId);

        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
    }

    [Fact]
    public async Task Get_Failed()
    {
        string userId = "TestID";

        _cacheService.Setup(s => s.GetAsync<List<AddProductRequest>>(
            It.Is<string>(i => i == userId))).ReturnsAsync((Func<List<AddProductRequest>>)null!);

        var result = await _basketService.Get(userId);

        result.Should().NotBeNull();
        result?.Data.Should().BeNull();
    }

    [Fact]
    public async Task AddProduct_Success()
    {
        string userId = "TestID";
        var addRequest = new AddProductRequest()
        {
            Id = 1,
            Name = "Test",
            Price = 1,
            Count = 1
        };

        int? testRes = 1;

        _cacheService.Setup(s => s.AddOrUpdateAsync(
            It.Is<string>(i => i == userId),
            It.Is<AddProductRequest>(i => i == addRequest))).ReturnsAsync(testRes);

        var result = await _basketService.AddProduct(userId, addRequest);

        result.Should().NotBeNull();
        result.Should().Be(testRes);
    }

    [Fact]
    public async Task AddProduct_Failed()
    {
        string userId = "TestID";
        var addRequest = new AddProductRequest()
        {
            Id = 1,
            Name = "Test",
            Price = 1,
            Count = 1
        };

        _cacheService.Setup(s => s.AddOrUpdateAsync(
            It.Is<string>(i => i == userId),
            It.Is<AddProductRequest>(i => i == addRequest))).ReturnsAsync((Func<int?>)null!);

        var result = await _basketService.AddProduct(userId, addRequest);

        result.Should().BeNull();
    }

    [Fact]
    public async Task RemoveProduct_Success()
    {
        string userId = "TestID";
        int productId = 1;

        int? testRes = 1;

        _cacheService.Setup(s => s.RemoveAsync(
            It.Is<string>(i => i == userId),
            It.Is<int>(i => i == productId))).ReturnsAsync(testRes);

        var result = await _basketService.RemoveProduct(userId, productId);

        result.Should().NotBeNull();
        result.Should().Be(testRes);
    }

    [Fact]
    public async Task RemoveProduct_Failed()
    {
        string userId = "TestID";
        int productId = 1;

        _cacheService.Setup(s => s.RemoveAsync(
            It.Is<string>(i => i == userId),
            It.Is<int>(i => i == productId))).ReturnsAsync((Func<int?>)null!);

        var result = await _basketService.RemoveProduct(userId, productId);

        result.Should().BeNull();
    }

    [Fact]
    public async Task ClearProducts_Success()
    {
        string userId = "TestID";

        _cacheService.Setup(s => s.ClearAsync(
            It.Is<string>(i => i == userId)));

        await _basketService.ClearProducts(userId);
    }
}
