namespace BlazorTrades.Data;

/// <summary>
/// Purpose of this class is to initialize a market data feed.
/// </summary>
public class EngineService
{
    private void OpenMarket()
    {
        Random tickerCount = new();
        MarketService market = new();
    }

    private void Start(MarketService market, Random tickerCount)
    {
        foreach (var ticker in market.CurrentDayTickers(tickerCount.Next(6, 20)))
        {
            var liquidityProvider = new LiquidityProvider(ticker.Name);
            var (price, volume) = liquidityProvider.GenerateTrade();
            
            
            // var stock = new Stock(ticker.Name, price, (int)volume, DateTime.Now);
            // Stocks.Add(stock);
        }
    }
}