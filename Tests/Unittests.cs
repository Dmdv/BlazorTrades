using BlazorTrades.Data;
using Xunit;
using FluentAssertions;
using Moq;

namespace BlazorTrades.Tests;

public class Unittests
{
    [Fact]
    public void AssumeLPGenerateOnlyPositive()
    {
        var lp = new LiquidityProvider("TEST");
        for (var i = 0; i < 100; i++)
        {
            var trade = lp.GenerateTrade();
            trade.Price.Should().BePositive();
            trade.Volume.Should().BePositive();
        }
    }
    
    [Fact]
    public void AssumeAllTickersFromMarketShouldHaveFourSymbols()
    {
        var ms = new MarketService();
        var tickers = ms.CurrentDayTickers(10);
        foreach (var ticker in tickers)
        {
            ticker.Name.Should().HaveLength(4);
        }
    }

    [Fact]
    public void AssumeMarketServiceCreatesOnlyGivenNumbers()
    {
        var ms = new MarketService();
        var tickers = ms.CurrentDayTickers(10);
        tickers.Should().HaveCount(10);
    }
    
    [Fact]
    public async Task AssumeTradingServiceGenerateStocks()
    {
        Mock<IHostApplicationLifetime> appLifetime = new();
        var ts = new TradingService(appLifetime.Object);
        var stocks = await ts.GetStockAsync();
        stocks.Should().HaveCountGreaterOrEqualTo(6);
        stocks.Should().HaveCountLessOrEqualTo(20);
    }
}