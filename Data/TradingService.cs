using System.Collections.Immutable;

namespace BlazorTrades.Data;

/// <summary>
/// Purpose of this class is to initialize a market data feed.
/// </summary>
public class TradingService
{
    private readonly IHostApplicationLifetime _appLifetime;
    private ImmutableList<StockMarketViewModel>? _stockViewModels;

    public TradingService(IHostApplicationLifetime appLifetime)
    {
        _appLifetime = appLifetime;
        OpenMarket();
    }

    private void OpenMarket()
    {
        Random tickerCount = new();
        MarketService market = new();

        Start(market, tickerCount);
    }

    private void Start(MarketService market, Random tickerCount)
    {
        _stockViewModels = market
            .CurrentDayTickers(tickerCount.Next(6, 20))
            .Select((ticker, i) =>
            {
                var liquidityProvider = new LiquidityProvider(ticker.Name);
                var (price, volume) = liquidityProvider.GenerateTrade();

                var stockMarket = new StockMarketViewModel(
                    new Stock(
                        i,
                        ticker.Name,
                        price,
                        volume,
                        volume,
                        0),
                    new LiquidityProvider(ticker.Name),
                    _appLifetime);

                Task.Run(() => stockMarket.Run());

                return stockMarket;
            }).ToImmutableList();
    }

    public ValueTask<ImmutableList<StockMarketViewModel>> GetStockAsync()
    {
        return ValueTask.FromResult(_stockViewModels!);
    }
}