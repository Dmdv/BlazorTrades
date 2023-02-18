namespace BlazorTrades.Data;

public record Ticker(string Name);

/// <summary>
/// Market service is used to simulate a market data feed.
/// </summary>
public class MarketService
{
    /// <summary>
    /// Current day tickers.
    /// </summary>
    public IEnumerable<Ticker> CurrentDayTickers(int count)
    {
        var random = new Random();

        for (var i = 0; i < count; i++)
        {
            var ticker = new char[4];
            for (var j = 0; j < 4; j++)
            {
                ticker[j] = (char)('A' + random.Next(0, 26));
            }
            
            
            yield return new Ticker(new string(ticker));
        }
    }
}